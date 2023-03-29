provider "aws" {
  region = "us-west-2"
}

locals {
  instance_type = "t2.micro"
}

resource "aws_dynamodb_table" "example" {
  name           = "example-table"
  billing_mode   = "PAY_PER_REQUEST"
  hash_key       = "id"
  read_capacity  = 5
  write_capacity = 5

  attribute {
    name = "id"
    type = "S"
  }
}

resource "aws_instance" "api_instance" {
  ami           = "<YOUR_AMI_ID>"
  instance_type = local.instance_type
  key_name      = "<YOUR_KEY_PAIR_NAME>"

  tags = {
    Name = "api-instance"
  }

  user_data = <<-EOF
              #!/bin/bash
              export DYNAMODB_TABLE=${aws_dynamodb_table.example.name}
              # Add any additional startup scripts or commands here
              EOF

  vpc_security_group_ids = [aws_security_group.allow_http_https.id]
}

resource "aws_security_group" "allow_http_https" {
  name        = "allow_http_https"
  description = "Allow inbound HTTP and HTTPS traffic"

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}
