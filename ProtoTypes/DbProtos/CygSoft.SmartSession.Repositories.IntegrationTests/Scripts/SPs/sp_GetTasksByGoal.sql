
/* ===================================================================================================
	Name:				sp_GetTasksByGoal
	CreatedBy:         	Rob Blake
	CreateDate:        	13/08/2018
	Modified by:       	Rob Blake

	Select tasks for a specific goal
	
Usage:
------------------------------------------------------------------------------------------------------
	EXEC SmartSession.dbo.sp_GetTasksByGoal 1
=================================================================================================== */

CREATE PROCEDURE dbo.sp_GetTasksByGoal
(
	@GoalId INT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT [Id]
		  ,[GoalId]
		  ,[Title]
		  ,[Description]
		  ,[CreateDate]
		  ,[GoalTaskType]
		  ,[DesiredSpeed]
	  FROM 
	[dbo].[GoalTask]
	WHERE
		GoalId = @GoalId

END

