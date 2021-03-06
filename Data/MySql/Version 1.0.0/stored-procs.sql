USE smartsession_qa;

/* ***********************************************************************************************************************
Exercise
*********************************************************************************************************************** */

DROP PROCEDURE IF EXISTS `sp_InsertExercise`;
CREATE PROCEDURE `sp_InsertExercise`
(
	in _title varchar(255), 
    in _targetMetronomeSpeed int(11),
    in _speedProgressWeighting int(11),
    in _targetPracticeTime int(11),
    in _practiceTimeProgressWeighting int(11),
    in _manualProgressWeighting int(11)
)
BEGIN
	INSERT INTO Exercise
    (
		Title, 
        TargetMetronomeSpeed, 
        SpeedProgressWeighting,
        TargetPracticeTime,
        PracticeTimeProgressWeighting,
        ManualProgressWeighting,
        DateCreated, 
        DateModified
	) 
	VALUES 
    (
		_title,
        _targetMetronomeSpeed,
        _speedProgressWeighting,
        _targetPracticeTime,
        _practiceTimeProgressWeighting,
        _manualProgressWeighting,
		NOW(), 
        NULL
	);
	SELECT LAST_INSERT_ID();
END;


DROP PROCEDURE IF EXISTS `sp_GetExerciseById`;
CREATE PROCEDURE `sp_GetExerciseById`(IN _id int)
BEGIN
	SELECT
		Id,
		Title, 
        TargetMetronomeSpeed, 
        SpeedProgressWeighting,
        TargetPracticeTime, 
        PracticeTimeProgressWeighting,
        ManualProgressWeighting,
        DateCreated, 
        DateModified
	FROM Exercise WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_DeleteExercise`;
CREATE PROCEDURE `sp_DeleteExercise`(in _id int)
BEGIN
	DELETE FROM Exercise WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_UpdateExercise`;
CREATE PROCEDURE `sp_UpdateExercise`(
	in _id int, 
	in _title varchar(255), 
	in _targetPracticeTime int,
    in _practiceTimeProgressWeighting int,
	in _targetMetronomeSpeed int,
    in _speedProgressWeighting int,
    in _manualProgressWeighting int
	)
BEGIN
	UPDATE Exercise SET 
		Title = _title,
		TargetPracticeTime = _targetPracticeTime,
        PracticeTimeProgressWeighting = _practiceTimeProgressWeighting,
		TargetMetronomeSpeed = _targetMetronomeSpeed,
        SpeedProgressWeighting = _speedProgressWeighting,
        ManualProgressWeighting = _manualProgressWeighting,
		DateModified = NOW()
	WHERE Id = _id;
END;


DROP PROCEDURE IF EXISTS `sp_FindExercises`;
CREATE PROCEDURE `sp_FindExercises`(
	in _title varchar(255),
	-- in _percentCompleteCalculationType int,
	in _fromDateCreated datetime,
	in _toDateCreated datetime,
	in _fromDateModified datetime,
	in _toDateModified datetime
	)
BEGIN
	SELECT * 
	FROM Exercise
	WHERE
		(_title IS NULL OR Title LIKE CONCAT('%', _title, '%'))
		-- AND
		-- (_percentCompleteCalculationType IS NULL OR PercentCompleteCalculationType = _percentCompleteCalculationType)
		AND
		(_fromDateCreated IS NULL OR DateCreated >= _fromDateCreated)
		AND
		(_toDateCreated IS NULL OR DateCreated <= _toDateCreated)
		AND
		(_fromDateModified IS NULL OR DateModified >= _fromDateModified)
		AND
		(_toDateModified IS NULL OR DateModified <= _toDateModified)
		;
END;

DROP PROCEDURE IF EXISTS `sp_GetPracticeRoutineExercises`;
CREATE PROCEDURE `sp_GetPracticeRoutineExercises`(
	in _practiceRoutineId int
	)
BEGIN
SELECT e.* 
FROM 
	exercise e
    INNER JOIN practiceroutineexercise pre on pre.ExerciseId = e.Id
WHERE
	pre.PracticeRoutineId = _practiceRoutineId;
END;

/* ***********************************************************************************************************************
ExerciseActivity
*********************************************************************************************************************** */

DROP PROCEDURE IF EXISTS `sp_InsertExerciseActivity`;
CREATE PROCEDURE `sp_InsertExerciseActivity`
(
	in _exerciseId int(11),
	in _seconds int(11),
	in _metronomeSpeed int(11),
    in _manualProgress int(11)
)
BEGIN
	INSERT INTO ExerciseActivity
    (
		ExerciseId,
		Seconds,
		MetronomeSpeed,
        ManualProgress,
		DateCreated
	) 
	VALUES 
    (
		_exerciseId,
		_seconds,
		_metronomeSpeed,
        _manualProgress,
		NOW()
	);
	SELECT LAST_INSERT_ID();
END;


DROP PROCEDURE IF EXISTS `sp_GetExerciseActivityById`;
CREATE PROCEDURE `sp_GetExerciseActivityById`(IN _id int)
BEGIN
	SELECT
		Id,
		ExerciseId,
		Seconds,
		MetronomeSpeed,
        ManualProgress,
		DateCreated,
		DateModified
	FROM ExerciseActivity WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_GetExerciseActivityByIds`;
CREATE PROCEDURE `sp_GetExerciseActivityByIds`(IN _ids TEXT)
BEGIN
	SELECT
		Id,
		ExerciseId,
		Seconds,
		MetronomeSpeed,
        ManualProgress,
		DateCreated,
		DateModified
	FROM ExerciseActivity 
    WHERE 
		FIND_IN_SET(ExerciseId, _ids) > 0
        ;
END;


    
DROP PROCEDURE IF EXISTS `sp_DeleteExerciseActivity`;
CREATE PROCEDURE `sp_DeleteExerciseActivity`(in _id int)
BEGIN
	DELETE FROM ExerciseActivity WHERE Id = _id;
END;


DROP PROCEDURE IF EXISTS `sp_UpdateExerciseActivity`;
CREATE PROCEDURE `sp_UpdateExerciseActivity`(
	_id int,
	_seconds int,
	_metronomeSpeed int,
    _manualProgress int
	)
BEGIN
	UPDATE ExerciseActivity SET 
		Seconds = _seconds,
		MetronomeSpeed = _metronomeSpeed,
        ManualProgress = _manualProgress,
		DateModified = NOW()
	WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_GetExerciseActivitiesByExercise`;
CREATE PROCEDURE `sp_GetExerciseActivitiesByExercise`(
	in _exerciseId int
	)
BEGIN
	SELECT  
		Id,
		ExerciseId,
		Seconds,
		MetronomeSpeed,
        ManualProgress,
		DateCreated,
		DateModified
	FROM ExerciseActivity
	WHERE
		ExerciseId = _exerciseId;
END;
COMMIT;

/* ***********************************************************************************************************************
PracticeRoutine
*********************************************************************************************************************** */

DROP PROCEDURE IF EXISTS `sp_InsertPracticeRoutine`;
CREATE PROCEDURE `sp_InsertPracticeRoutine`
(
	in _title varchar(255)
)
BEGIN
	INSERT INTO PracticeRoutine
    (
		Title, 
        DateCreated,
        DateModified
	) 
	VALUES 
    (
		_title,
		NOW(), 
        NULL
	);
	SELECT LAST_INSERT_ID();
END;

DROP PROCEDURE IF EXISTS `sp_GetPracticeRoutineById`;
CREATE PROCEDURE `sp_GetPracticeRoutineById`(IN _id int)
BEGIN
	SELECT
		Id,
		Title, 
        DateCreated, 
        DateModified
	FROM PracticeRoutine WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_FindPracticeRoutines`;
CREATE PROCEDURE `sp_FindPracticeRoutines`(
	in _title varchar(255),
	in _fromDateCreated datetime,
	in _toDateCreated datetime,
	in _fromDateModified datetime,
	in _toDateModified datetime
	)
BEGIN
	SELECT * 
	FROM PracticeRoutine
	WHERE
		(_title IS NULL OR Title LIKE CONCAT('%', _title, '%'))
		-- AND
		-- (_percentCompleteCalculationType IS NULL OR PercentCompleteCalculationType = _percentCompleteCalculationType)
		AND
		(_fromDateCreated IS NULL OR DateCreated >= _fromDateCreated)
		AND
		(_toDateCreated IS NULL OR DateCreated <= _toDateCreated)
		AND
		(_fromDateModified IS NULL OR DateModified >= _fromDateModified)
		AND
		(_toDateModified IS NULL OR DateModified <= _toDateModified)
		;
END;

DROP PROCEDURE IF EXISTS `sp_DeletePracticeRoutine`;
CREATE PROCEDURE `sp_DeletePracticeRoutine`(in _id int)
BEGIN
	SET SQL_SAFE_UPDATES=0;
	
	DELETE FROM TimeSlotExercise WHERE TimeSlotId IN (SELECT TimeSlotId FROM PracticeRoutineTimeSlot WHERE PracticeRoutineId = _id);
	DELETE FROM PracticeRoutineTimeSlot WHERE PracticeRoutineId = _id;
	DELETE FROM TimeSlot WHERE Id IN (SELECT TimeSlotId FROM PracticeRoutineTimeSlot WHERE PracticeRoutineId = _id);
	DELETE FROM PracticeRoutine WHERE Id = _id;
	
	SET SQL_SAFE_UPDATES=1;
END;

DROP PROCEDURE IF EXISTS `sp_UpdatePracticeRoutine`;
CREATE PROCEDURE `sp_UpdatePracticeRoutine`(
	in _id int, 
	in _title varchar(255)
	)
BEGIN
	UPDATE PracticeRoutine SET 
		Title = _title,
		DateModified = NOW()
	WHERE Id = _id;
END;


/* ***********************************************************************************************************************
PracticeRoutineExerciseRecorder
*********************************************************************************************************************** */

DROP PROCEDURE IF EXISTS `sp_GetPracticeRoutineExerciseRecordersByRoutineId`;
CREATE PROCEDURE `sp_GetPracticeRoutineExerciseRecordersByRoutineId`(
	in _practiceRoutineId int
	)
BEGIN

	DROP TEMPORARY TABLE IF EXISTS RandomRoutineSlotExercises;
	DROP TEMPORARY TABLE IF EXISTS LastMetronomeSpeeds;
	DROP TEMPORARY TABLE IF EXISTS FirstMetronomeSpeeds;
	
	CREATE TEMPORARY TABLE RandomRoutineSlotExercises
	SELECT 
		PR.Id AS PracticeRoutineId, 
		T.Id As TimeSlotId, 
		(
			SELECT Id 
			FROM 
				Exercise E1
				INNER JOIN TimeSlotExercise TSE1 ON TSE1.ExerciseId = E1.Id
			WHERE
				TSE1.TimeSlotId = T.Id
			-- https://www.alvinpoh.com/how-to-randomly-select-from-a-record-based-on-weight-php-mysql/
            -- Random, but weighted.
			ORDER BY LOG(RAND()) / TSE1.FrequencyWeighting DESC LIMIT 1
		) AS ExerciseId
	FROM 
		PracticeRoutine PR 
		INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.PracticeRoutineId = PR.Id
		INNER JOIN TimeSlot T ON T.Id = PRT.TimeSlotId
		INNER JOIN TimeSlotExercise TSE ON TSE.TimeSlotId = T.Id
		INNER JOIN Exercise E ON E.Id = TSE.ExerciseId
	WHERE
		PR.Id = _practiceRoutineId 
	GROUP BY
		PR.Id,
		T.Id;

	CREATE TEMPORARY TABLE LastMetronomeSpeeds
	SELECT DISTINCT
		E.Id AS ExerciseId, 
		EA.MetronomeSpeed AS LastRecordedSpeed,
		EA.ManualProgress AS LastRecordedManualProgress,
        (
			select 
				sum(ea1.seconds) 
			from
				exerciseactivity ea1
				inner join exercise e1 on e1.id = ea1.exerciseid
			where
				e1.id = e.id
        ) TotalSeconds
	FROM
		ExerciseActivity EA
		INNER JOIN Exercise E ON E.Id = EA.ExerciseId
		INNER JOIN TimeSlotExercise TSE ON TSE.ExerciseId = E.Id
		INNER JOIN TimeSlot T ON T.Id = TSE.TimeSlotId
		INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.TimeSlotId = T.Id
		INNER JOIN PracticeRoutine PR ON PR.Id = PRT.PracticeRoutineId
		INNER JOIN
		(
			SELECT
				Ex.Id, 
				MAX(EAx.Id) AS LastId
			FROM
				ExerciseActivity EAx
				INNER JOIN Exercise Ex ON Ex.Id = EAx.ExerciseId
				INNER JOIN TimeSlotExercise TSEx ON TSEx.ExerciseId = Ex.Id
				INNER JOIN TimeSlot Tx ON Tx.Id = TSEx.TimeSlotId
				INNER JOIN PracticeRoutineTimeSlot PRTx ON PRTx.TimeSlotId = Tx.Id
				INNER JOIN PracticeRoutine PRx ON PRx.Id = PRTx.PracticeRoutineId
			WHERE
				PRx.Id = _practiceRoutineId 
			GROUP BY
				Ex.Id
		) LS ON LS.Id = EA.ExerciseId AND LS.LastId = EA.Id
	WHERE
		PR.Id = _practiceRoutineId 
	GROUP BY
		E.Id, 
		EA.MetronomeSpeed, 
		EA.ManualProgress
	;
	
	CREATE TEMPORARY TABLE FirstMetronomeSpeeds
	SELECT DISTINCT
		E.Id AS ExerciseId, 
		EA.MetronomeSpeed AS InitialRecordedSpeed
	FROM
		ExerciseActivity EA
		INNER JOIN Exercise E ON E.Id = EA.ExerciseId
		INNER JOIN TimeSlotExercise TSE ON TSE.ExerciseId = E.Id
		INNER JOIN TimeSlot T ON T.Id = TSE.TimeSlotId
		INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.TimeSlotId = T.Id
		INNER JOIN PracticeRoutine PR ON PR.Id = PRT.PracticeRoutineId
		INNER JOIN
		(
			SELECT
				Ex.Id, 
				MIN(EAx.Id) AS LastId
			FROM
				ExerciseActivity EAx
				INNER JOIN Exercise Ex ON Ex.Id = EAx.ExerciseId
				INNER JOIN TimeSlotExercise TSEx ON TSEx.ExerciseId = Ex.Id
				INNER JOIN TimeSlot Tx ON Tx.Id = TSEx.TimeSlotId
				INNER JOIN PracticeRoutineTimeSlot PRTx ON PRTx.TimeSlotId = Tx.Id
				INNER JOIN PracticeRoutine PRx ON PRx.Id = PRTx.PracticeRoutineId
			WHERE
				PRx.Id = _practiceRoutineId 
			GROUP BY
				Ex.Id
		) LS ON LS.Id = EA.ExerciseId AND LS.LastId = EA.Id
	WHERE
		PR.Id = _practiceRoutineId ;
		
	SELECT 
		PR.Id AS PracticeRoutineId,
		E.Id AS ExerciseId,
		T.Title AS TimeSlotTitle,
		E.Title AS ExerciseTitle,

		E.ManualProgressWeighting,
		E.SpeedProgressWeighting,
		E.PracticeTimeProgressWeighting,
			
		IFNULL(FM.InitialRecordedSpeed, 0) AS InitialRecordedSpeed,
		IFNULL(LM.LastRecordedSpeed, 0) AS LastRecordedSpeed,
		IFNULL(LM.LastRecordedManualProgress, 0) AS LastRecordedManualProgress,
		IFNULL(LM.TotalSeconds, 0) AS TotalPracticeTime,
		IFNULL(T.AssignedPracticeTime, 0) AS AssignedPracticeTime,
		E.TargetMetronomeSpeed AS TargetMetronomeSpeed,
		E.TargetPracticeTime AS TargetPracticeTime,
		PR.DateCreated,
		PR.DateModified
		
	FROM
		RandomRoutineSlotExercises TMP
		INNER JOIN PracticeRoutine PR ON PR.Id = TMP.PracticeRoutineId
		INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.PracticeRoutineId = PR.Id
		INNER JOIN TimeSlot T ON T.Id = PRT.TimeSlotId
		INNER JOIN TimeSlotExercise TSE ON TSE.TimeSlotId = T.Id
		INNER JOIN Exercise E ON E.Id = TSE.ExerciseId AND E.Id = TMP.ExerciseId
		LEFT JOIN LastMetronomeSpeeds LM ON LM.ExerciseId = E.Id
		LEFT JOIN FirstMetronomespeeds FM ON FM.ExerciseId = E.Id
	;
	
	DROP TEMPORARY TABLE IF EXISTS RandomRoutineSlotExercises;
	DROP TEMPORARY TABLE IF EXISTS LastMetronomeSpeeds;
	DROP TEMPORARY TABLE IF EXISTS FirstMetronomeSpeeds;

END;
COMMIT;



DROP PROCEDURE IF EXISTS `sp_GetExerciseRecorderByExerciseId`;
CREATE PROCEDURE `sp_GetExerciseRecorderByExerciseId`(
	in _exerciseId int
	)
BEGIN

	DROP TEMPORARY TABLE IF EXISTS LastMetronomeSpeeds;
	DROP TEMPORARY TABLE IF EXISTS FirstMetronomeSpeeds;
	
	CREATE TEMPORARY TABLE LastMetronomeSpeeds
	SELECT DISTINCT
		E.Id AS ExerciseId, 
		EA.MetronomeSpeed AS LastRecordedSpeed,
		EA.ManualProgress AS LastRecordedManualProgress,
        (
			select 
				sum(ea1.seconds) 
			from
				exerciseactivity ea1
				inner join exercise e1 on e1.id = ea1.exerciseid
			where
				e1.id = e.id
        ) TotalSeconds
	FROM
		ExerciseActivity EA
		INNER JOIN Exercise E ON E.Id = EA.ExerciseId
		INNER JOIN
		(
			SELECT
				Ex.Id, 
				MAX(EAx.Id) AS LastId
			FROM
				ExerciseActivity EAx
				INNER JOIN Exercise Ex ON Ex.Id = EAx.ExerciseId
			WHERE
				Ex.Id = _exerciseId 
			GROUP BY
				Ex.Id
		) LS ON LS.Id = EA.ExerciseId AND LS.LastId = EA.Id
	WHERE
		E.Id = _exerciseId 
	GROUP BY
		E.Id, 
		EA.MetronomeSpeed, 
		EA.ManualProgress
	;

	CREATE TEMPORARY TABLE FirstMetronomeSpeeds
	SELECT DISTINCT
		E.Id AS ExerciseId, 
		EA.MetronomeSpeed AS InitialRecordedSpeed
	FROM
		ExerciseActivity EA
		INNER JOIN Exercise E ON E.Id = EA.ExerciseId
		INNER JOIN
		(
			SELECT
				Ex.Id, 
				MIN(EAx.Id) AS LastId
			FROM
				ExerciseActivity EAx
				INNER JOIN Exercise Ex ON Ex.Id = EAx.ExerciseId
			WHERE
				Ex.Id = _exerciseId
			GROUP BY
				Ex.Id
		) LS ON LS.Id = EA.ExerciseId AND LS.LastId = EA.Id
	WHERE
		E.Id = _exerciseId 
	;
    
	SELECT 
		E.Id AS ExerciseId,
		E.Title AS ExerciseTitle,

		E.ManualProgressWeighting,
		E.SpeedProgressWeighting,
		E.PracticeTimeProgressWeighting,
			
		IFNULL(FM.InitialRecordedSpeed, 0) AS InitialRecordedSpeed,
		IFNULL(LM.LastRecordedSpeed, 0) AS LastRecordedSpeed,
		IFNULL(LM.LastRecordedManualProgress, 0) AS LastRecordedManualProgress,
		IFNULL(LM.TotalSeconds, 0) AS TotalPracticeTime,
		E.TargetMetronomeSpeed AS TargetMetronomeSpeed,
		E.TargetPracticeTime AS TargetPracticeTime
		
	FROM
		Exercise E
		LEFT JOIN LastMetronomeSpeeds LM ON LM.ExerciseId = E.Id
		LEFT JOIN FirstMetronomespeeds FM ON FM.ExerciseId = E.Id
	WHERE
		E.Id = _exerciseId
	;

	DROP TEMPORARY TABLE IF EXISTS LastMetronomeSpeeds;
	DROP TEMPORARY TABLE IF EXISTS FirstMetronomeSpeeds;

END;
COMMIT;

-- CALL sp_GetTimeSlotExerciseByTimeSlotIds('1,2,3');
DROP PROCEDURE IF EXISTS `sp_GetTimeSlotExerciseByTimeSlotIds`;
CREATE PROCEDURE `sp_GetTimeSlotExerciseByTimeSlotIds`(IN _ids TEXT)
BEGIN
	SELECT
		E.Id,
		T.Id AS TimeSlotId,
		E.Title,
		TSE.FrequencyWeighting
	FROM 
			TimeSlot T
			INNER JOIN TimeSlotExercise TSE ON TSE.TimeSlotId = T.Id
			INNER JOIN Exercise E ON E.Id = TSE.ExerciseId
	WHERE
		FIND_IN_SET(T.Id, _ids) > 0
	;
END;

DROP PROCEDURE IF EXISTS `sp_GetTimeSlotExerciseByTimeSlotId`;
CREATE PROCEDURE `sp_GetTimeSlotExerciseByTimeSlotId`(IN _id INT)
BEGIN
	SELECT
		E.Id,
		T.Id AS TimeSlotId,
		E.Title,
		TSE.FrequencyWeighting
	FROM 
			TimeSlot T
			INNER JOIN TimeSlotExercise TSE ON TSE.TimeSlotId = T.Id
			INNER JOIN Exercise E ON E.Id = TSE.ExerciseId
	WHERE
		T.Id = _id
	;
END;


/* --------------------------------------------------------------------------------------
Currently not being used.
-------------------------------------------------------------------------------------- */
DROP PROCEDURE IF EXISTS `sp_GetTimeSlotExerciseById`;
CREATE PROCEDURE `sp_GetTimeSlotExerciseById`(IN _timeSlotId int, IN _exerciseId int)
BEGIN
	SELECT
		E.Id,
		T.Id AS TimeSlotId,
		E.Title,
		TSE.FrequencyWeighting
	FROM 
			TimeSlot T
			INNER JOIN TimeSlotExercise TSE ON TSE.TimeSlotId = T.Id
			INNER JOIN Exercise E ON E.Id = TSE.ExerciseId
	WHERE
		T.Id = _timeSlotId AND E.Id = _exerciseId 
	;
END;
/* ------------------------------------------------------------------------------------ */


DROP PROCEDURE IF EXISTS `sp_GetTimeSlotsByPracticeRoutineId`;
CREATE PROCEDURE `sp_GetTimeSlotsByPracticeRoutineId`(IN _id int)
BEGIN
	SELECT 
		T.Id,
		T.Title,
		T.AssignedPracticeTime
	FROM 
		PracticeRoutine PR 
		INNER JOIN PracticeRoutineTimeSlot PRT ON PRT.PracticeRoutineId = PR.Id
		INNER JOIN TimeSlot T ON T.Id = PRT.TimeSlotId
	WHERE
		PracticeRoutineId = _id;
END;




DROP PROCEDURE IF EXISTS `sp_InsertTimeSlotExercise`;
CREATE PROCEDURE `sp_InsertTimeSlotExercise`
(
	in _exerciseId int(11),
	in _timeSlotId int(11),
	in _frequencyWeighting int(11)
)
BEGIN
	INSERT INTO TimeSlotExercise
    (
		ExerciseId,
		TimeSlotId,
		FrequencyWeighting,
		DateCreated,
		DateModified
	) 
	VALUES 
    (
		_exerciseId,
		_timeSlotId,
		_frequencyWeighting,
		NOW(),
		NULL
	);
	SELECT LAST_INSERT_ID();
END;


DROP PROCEDURE IF EXISTS `sp_InsertTimeSlot`;
CREATE PROCEDURE `sp_InsertTimeSlot`
(
	in _title varchar(255),
	in _practiceRoutineId int(11),
	in _assignedPracticeTime int(11)
)
BEGIN

	DECLARE timeSlotId INT DEFAULT 0;

	INSERT INTO TimeSlot 
	(
		Title, 
		AssignedPracticeTime, 
		DateCreated, 
		DateModified
	) VALUES 
	(
		_title, 
		_assignedPracticeTime, 
		NOW(), 
		NULL
	);
	
	SELECT LAST_INSERT_ID() INTO timeSlotId;
	
	INSERT INTO PracticeRoutineTimeslot 
	(
		PracticeRoutineId, 
		TimeSlotId, 
		DateCreated, 
		DateModified
	)
	VALUES 
	(
		_practiceRoutineId, 
		timeSlotId, 
		NOW(), 
		NULL
	);
	
	SELECT timeSlotId;
	
END;


DROP PROCEDURE IF EXISTS `sp_UpdateTimeSlot`;
CREATE PROCEDURE `sp_UpdateTimeSlot`(
	in _id int, 
	in _title varchar(255), 
	in _assignedPracticeTime int
	)
BEGIN
	UPDATE TimeSlot
    SET 
		Title = _title,
		AssignedPracticeTime = _assignedPracticeTime,
		DateModified = NOW()
	WHERE 
		Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_UpdateTimeSlotExercise`;
CREATE PROCEDURE `sp_UpdateTimeSlotExercise`(
	in _exerciseId int, 
    in _timeSlotId int, 
	in _frequencyWeighting int
	)
BEGIN
	UPDATE TimeSlotExercise 
    SET 
		FrequencyWeighting = _frequencyWeighting,
		DateModified = NOW()
	WHERE 
		TimeSlotId = _timeSlotId AND ExerciseId = _exerciseId;
END;

-- call sp_DeleteTimeSlot(1);
DROP PROCEDURE IF EXISTS `sp_DeleteTimeSlot`;
CREATE PROCEDURE `sp_DeleteTimeSlot`(in _id int)
BEGIN
	DELETE FROM PracticeRoutineTimeSlot 
		WHERE TimeSlotId = _id;
	DELETE FROM TimeSlotExercise 
		WHERE TimeSlotId = _id;
	DELETE FROM TimeSlot 
		WHERE Id = _id;
END;

DROP PROCEDURE IF EXISTS `sp_DeleteTimeSlotExercisesByIds`;
CREATE PROCEDURE `sp_DeleteTimeSlotExercisesByIds`(IN _timeSlotId INT, IN _exerciseIds TEXT)
BEGIN
	DELETE FROM TimeSlotExercise 
		WHERE 
			TimeSlotId = _timeSlotId AND 
			FIND_IN_SET(ExerciseId, _exerciseIds) > 0;
	
END;