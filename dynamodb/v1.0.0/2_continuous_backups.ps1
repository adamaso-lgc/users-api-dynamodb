<#
    INFO:
        This script is intended for cloud environment, e.g. DEVQA where it attempts to
		Adds continuous backups to tables
						
#>
Write-Host "Creating tables continuous backups..."
$tableregions = @(
	@{tablename='users_ilab';region='eu-west-1'},
	@{tablename='users_ilab';region='us-east-1'},
)
foreach($tableregionsItem in $tableregions)
{	
	Write-Host ("TABLE:" + $tableregionsItem.tablename + " REGION:" + $tableregionsItem.region)	
	
	aws dynamodb update-continuous-backups `
		--table-name $tableregionsItem.tablename `
		--point-in-time-recovery-specification PointInTimeRecoveryEnabled=true ` # This line will most likely need to be removed, and PITR should be added manually by devops team
		--region $tableregionsItem.region


}
