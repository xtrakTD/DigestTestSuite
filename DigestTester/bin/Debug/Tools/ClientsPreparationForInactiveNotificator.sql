USE svadba_catalog
GO

--#region [ServerConfig]
EXEC sp_configure 'show advanced options', 1;  
GO  

RECONFIGURE;  
GO  
 
EXEC sp_configure 'xp_cmdshell', 1;  
GO  

RECONFIGURE;  
GO  

EXEC sp_configure 'show advanced options', 0;  
GO  

RECONFIGURE;  
GO  
--#endregion

--#region [ClientConfigOptions]

DECLARE @ResetConfig BIT = 0
DECLARE @CurrentPulse INT = 130
DECLARE @PulseRange INT = 2
DECLARE @SDVUser NVARCHAR(50) = N'd.minin'

DECLARE @MakeRecentClickedNotification BIT = 0
DECLARE @MakeOldNotificationClickDays INT = 5

DECLARE @MakeRecentSessions BIT = 0
DECLARE @MakeOldSessionsDays INT = 6

DECLARE @MakeConfirmedEmail BIT = 0
DECLARE @MakeUnconfirmedClient BIT = 1

DECLARE @MakeOldClient BIT = 1
DECLARE @MakeNewClient BIT = 0

DECLARE @MakePass9MonthsRule BIT = 1
DECLARE @MakeFail9MonthsRule BIT = 0

DECLARE @UseCustomIDList BIT = 1

DECLARE @Ids TABLE(UserId BIGINT UNIQUE NOT NULL)
INSERT INTO @Ids(UserId) 
SELECT UserId FROM tblClients WITH(NOLOCK) WHERE UserId IN(
		10035015674, 10035015678, 10035015684, 10035015690, 10035015695, 10035015700, 10035015705, 10035015708, 10035015715, 10035015718, 10035015724, 10035015728, 10035015732, 10035015739, 10035015742, 
		10035015747, 10035015752, 10035015755, 10035015760, 10035015764, 10035015770, 10035015775, 10035015780, 10035015785, 10035015791, 10035015795, 10035015800, 10035015804, 10035015809, 10035015815, 
		10035015819, 10035015823, 10035015829, 10035015833, 10035015838, 10035015844, 10035015849, 10035015853, 10035015857, 10035015862, 10035015867, 10035015874, 10035015878, 10035015885, 10035015888, 
		10035015893, 10035015898, 10035015903, 10035015673, 10035015679, 10035015685, 10035015689, 10035015694, 10035015698, 10035015703, 10035015709, 10035015714, 10035015717, 10035015722, 10035015727, 
		10035015733, 10035015741, 10035015746, 10035015751, 10035015757, 10035015761, 10035015766, 10035015769, 10035015777, 10035015782, 10035015787, 10035015792, 10035015797, 10035015802, 10035015805, 
		10035015810, 10035015814, 10035015817, 10035015822, 10035015826, 10035015831, 10035015836, 10035015842, 10035015847, 10035015854, 10035015858, 10035015863, 10035015868, 10035015873, 10035015879, 
		10035015884, 10035015890, 10035015894, 10035015899, 10035015904, 10035015908, 10035015677, 10035015680, 10035015683, 10035015688, 10035015693, 10035015699, 10035015704, 10035015710, 10035015713, 
		10035015719, 10035015723, 10035015730, 10035015734, 10035015738, 10035015744, 10035015749, 10035015753, 10035015758, 10035015767, 10035015772, 10035015776, 10035015781, 10035015786, 10035015790, 
		10035015796, 10035015799, 10035015806, 10035015811, 10035015816, 10035015821, 10035015827, 10035015832, 10035015837, 10035015841, 10035015846, 10035015851, 10035015856, 10035015861, 10035015866, 
		10035015871, 10035015876, 10035015881, 10035015886, 10035015891, 10035015896, 10035015901, 10035015906, 10035015909, 10035015675, 10035015681, 10035015686, 10035015691, 10035015696, 10035015701, 
		10035015706, 10035015711, 10035015716, 10035015721, 10035015726, 10035015731, 10035015736, 10035015740, 10035015745, 10035015750, 10035015756, 10035015759, 10035015763, 10035015768, 10035015773, 
		10035015778, 10035015784, 10035015789, 10035015794, 10035015801, 10035015807, 10035015812, 10035015820, 10035015825, 10035015830, 10035015835, 10035015840, 10035015845, 10035015850, 10035015855, 
		10035015860, 10035015864, 10035015869, 10035015872, 10035015877, 10035015882, 10035015887, 10035015892, 10035015897, 10035015902, 10035015907, 10035015911, 10035015676, 10035015682, 10035015687, 
		10035015692, 10035015697, 10035015702, 10035015707, 10035015712, 10035015720, 10035015725, 10035015729, 10035015735, 10035015737, 10035015743, 10035015748, 10035015754, 10035015762, 10035015765, 
		10035015771, 10035015774, 10035015779, 10035015783, 10035015788, 10035015793, 10035015798, 10035015803, 10035015808, 10035015813, 10035015818, 10035015824, 10035015828, 10035015834, 10035015839, 
		10035015843, 10035015848, 10035015852, 10035015859, 10035015865, 10035015870, 10035015875, 10035015880, 10035015883, 10035015889, 10035015895, 10035015900, 10035015905, 10035015910, 10035015912
)

--#endregion

--#region [RunOnceConfiguration]

DECLARE @RunOnce BIT
DECLARE @ZeroPulseTime DATETIME

IF(OBJECT_ID(N'tempdb..#InactiveUsersDigestScriptConfigDone', N'U') IS NULL) 
	CREATE TABLE #InactiveUsersDigestScriptConfigDone(Done BIT NOT NULL, ZeroPulseTime DATETIME, DateOfAdded DATETIME NOT NULL)

SET @RunOnce = ISNULL((SELECT TOP 1 Done FROM #InactiveUsersDigestScriptConfigDone WHERE Done = 1), 0) 

IF(ISNULL(@ResetConfig, 0) = 1)
BEGIN
	DELETE FROM #InactiveUsersDigestScriptConfigDone WHERE DateOfAdded IS NOT NULL
END

IF(ISNULL(@RunOnce, 0) = 0 OR ISNULL(@ResetConfig, 0) = 1)
BEGIN	
	SET @ZeroPulseTime = (SELECT DATEADD(MINUTE, -(6 * @CurrentPulse), GETUTCDATE()))		

	INSERT INTO #InactiveUsersDigestScriptConfigDone(Done, ZeroPulseTime, DateOfAdded)
	VALUES(1, @ZeroPulseTime, GETUTCDATE())
END 
ELSE BEGIN
	PRINT 'Configuration already done'
END

--#endregion

--#region [SetVariables]

DECLARE @ZeroPulse INT
IF(@ZeroPulseTime IS NULL)
BEGIN
	SET @ZeroPulseTime = (SELECT TOP 1 ZeroPulseTime FROM #InactiveUsersDigestScriptConfigDone WITH(NOLOCK) ORDER BY DateOfAdded DESC)
END

SET @ZeroPulse = (DATEDIFF(MINUTE, @ZeroPulseTime, GETUTCDATE()) / 6) + 1
DECLARE @FiredPulses INT = 0

--#endregion

--#region [GetUsersForDigest]

IF OBJECT_ID(N'tempdb..#users', N'U') IS NOT NULL
  DROP TABLE #users

CREATE TABLE #users(idx SMALLINT Primary Key IDENTITY(1, 1), UserId BIGINT UNIQUE NOT NULL)

WHILE (@FiredPulses < @PulseRange)
BEGIN
	DECLARE @PulseId INT = @ZeroPulse + @FiredPulses	

	DECLARE @tempClients TABLE(UserId BIGINT UNIQUE NOT NULL)
	INSERT INTO @tempClients(UserId)	
	EXEC Letters.GetUsersForDigest @PortionSize=1000,@UserIdRemainder=@PulseId,@UserIdDivisor=240

	INSERT INTO #users(UserId)
	SELECT DISTINCT tc.UserId FROM @tempClients tc WHERE tc.UserId IN (
		SELECT UserId FROM @Ids
	) AND 
	tc.UserId NOT IN (
		SELECT UserId FROM #users WITH(NOLOCK)
	)

	SET @FiredPulses = @FiredPulses + 1
END

DECLARE @time DATETIME = GETUTCDATE()
DECLARE @recentSessionTime DATETIME = DATEADD(HOUR, -12, GETUTCDATE())

--#endregion

--#region [ChangeUserEmails]

DECLARE @i INT = 1
WHILE (@i <= (SELECT MAX(idx) FROM #users))
BEGIN
	DECLARE @user BIGINT = (SELECT UserId FROM #users WHERE idx = @i)

    DECLARE @r VARCHAR(8)
    
	SELECT @r = COALESCE(@r , '' ) + n FROM (
      SELECT TOP 8 CHAR(NUMBER) n FROM MASTER..spt_values    
      WHERE TYPE = 'P' AND
      (NUMBER BETWEEN ASCII(0) AND ASCII(9)
      OR NUMBER BETWEEN ASCII('A') AND ASCII('Z')
      OR NUMBER BETWEEN ASCII('a') AND ASCII('a'))
      ORDER BY NEWID()) a

	DECLARE @Email NVARCHAR(MAX) = @SDVUser + '_' + @r + N'@sdventures.com'

  UPDATE tblClients
    SET EmailAddress = @Email
    WHERE UserId = (SELECT UserId FROM #users WHERE idx = @i)

--#endregion

--#region [@MakeRecentSessions]

	IF(ISNULL(@MakeRecentSessions, 0) = 1)
	BEGIN
		IF EXISTS(SELECT ClientID FROM dbo.tblClientSessions_Archive WITH(NOLOCK) WHERE ClientId = (
			SELECT ClientId FROM tblClients WHERE UserId = (
				SELECT UserId FROM #users WHERE idx = @i
				)))
			BEGIN
				UPDATE tblClientSessions_Archive
				SET LastTs = DATEADD(HOUR, -10, GETUTCDATE()), LoginTs = DATEADD(HOUR, -12, GETUTCDATE())
				WHERE ClientId = (
					SELECT ClientId FROM tblClients WHERE UserId = (
						SELECT UserId FROM #users WHERE idx = @i
				)) 
				AND LastTs IN
				(
					SELECT TOP 1 LastTs FROM dbo.tblClientSessions_Archive WITH(NOLOCK) WHERE ClientID = (
						SELECT ClientId FROM tblClients WHERE UserId = (
							SELECT UserId FROM #users WHERE idx = @i
					)) ORDER BY LastTs DESC
				)
			END
			ELSE BEGIN
				DECLARE @ClientId INT = (SELECT ClientId FROM tblClients WHERE UserId = (SELECT UserId FROM #users WHERE idx = @i))
				INSERT INTO tblClientSessions_Archive(LoginTs, ClientID, LastTs)
				VALUES(DATEADD(HOUR, -12, GETUTCDATE()), @ClientId, DATEADD(HOUR, -10, GETUTCDATE()))        
			END

			EXEC ElectronicMail.SetLastSession @UserId=@user,@Timestamp=@recentSessionTime
	END

--#endregion

--#region [@MakeOldSessionsDays]

	IF(ISNULL(@MakeOldSessionsDays, 0) > 0)
	BEGIN
		DECLARE @timestamp DATETIME = DATEADD(DAY, -@MakeOldSessionsDays, GETUTCDATE())
		EXEC ElectronicMail.DeleteLastSession @UserId=@user
		EXEC ElectronicMail.SetLastSession @UserId=@user,@Timestamp=@timestamp
	END

--#endregion

--#region [@MakeRecentClickedNotification]

	IF(ISNULL(@MakeRecentClickedNotification, 0) = 1)
	BEGIN
		EXEC [ElectronicMail].[UnconfirmedInfo.Delete] @UserId=@user
		IF((SELECT TOP 1 ISNULL(Confirmed, 0) FROM MailingSystem.UserEmails WITH(NOLOCK) WHERE Email = @Email AND UserIdentity = @user ORDER BY ISNULL(Confirmed, 0) DESC) = 1)
		BEGIN 
			EXEC ElectronicMail.ProlongConfirmation @Email=@Email,@ConfirmationDate=@recentSessionTime
		END
		ELSE BEGIN
			EXEC MailingSystem.ConfirmUserEmail @UserId=@user,@Email=@Email,@ConfirmationDate=@recentSessionTime,@Ip=N'127.0.0.1',@Referrer=NULL
		END
	END

--#endregion

--#region [@MakeOldNotificationClickDays]

	IF(ISNULL(@MakeOldNotificationClickDays, 0) > 0)
	BEGIN
		DECLARE @customDate DATETIME = DATEADD(DAY, -@MakeOldNotificationClickDays, GETUTCDATE())
		DECLARE @ancient DATETIME = '1900-01-01 00:00:00.000'

		EXEC [ElectronicMail].[UnconfirmedInfo.Delete] @UserId=@user
		IF((SELECT TOP 1 (ISNULL(ConfirmationDate, @ancient)) FROM MailingSystem.UserEmails WITH(NOLOCK) 
			WHERE Email = @Email AND UserIdentity = @user ORDER BY ISNULL(ConfirmationDate, @ancient) DESC) > @customDate)
			BEGIN
				UPDATE MailingSystem.UserEmails
				SET ConfirmationDate = @ancient WHERE UserIdentity = @user AND Email = @Email
			END

		IF((SELECT TOP 1 ISNULL(Confirmed, 0) FROM MailingSystem.UserEmails WITH(NOLOCK) WHERE Email = @Email AND UserIdentity = @user ORDER BY ISNULL(ConfirmationDate, @ancient) DESC) = 1)
		BEGIN 
			EXEC ElectronicMail.ProlongConfirmation @Email=@Email,@ConfirmationDate=@MakeOldNotificationClickDays
		END
		ELSE BEGIN
			EXEC MailingSystem.ConfirmUserEmail @UserId=@user,@Email=@Email,@ConfirmationDate=@MakeOldNotificationClickDays,@Ip=N'127.0.0.1',@Referrer=NULL
		END
	END

--#endregion

--#region [@MakePass9MonthsRule]

	IF(ISNULL(@MakePass9MonthsRule, 0) = 1)
	BEGIN
		DECLARE @oldButPassableTime DATETIME = DATEADD(DAY, -200, GETUTCDATE())
		DECLARE @lastActivationDate DATETIME
		EXEC @LastActivationDate = ElectronicMail.GetLastActivationDate @UserId=@user,@MinActivationDate=@ancient

		IF (SELECT DateOfAdded FROM tblClients WITH(NOLOCK) WHERE UserID = @user) < DATEADD(DAY, -275, GETUTCDATE())
		BEGIN
			IF NOT EXISTS(SELECT TOP 1 Email FROM MailingSystem.UserEmails WHERE UserIdentity = @user AND Email = @Email)
			BEGIN
				EXEC MailingSystem.ConfirmUserEmail @UserId=@user,@Email=@Email,@ConfirmationDate=@oldButPassableTime,@Ip=N'127.0.0.1',@Referrer=NULL
				EXEC ElectronicMail.SetLastSession @UserId=@user,@TimeStamp=@oldButPassableTime

				IF(@LastActivationDate < DATEADD(DAY, -275, GETUTCDATE()))
				BEGIN
					UPDATE MailingSystem.UserEmails
					SET ConfirmationDate = @oldButPassableTime WHERE UserIdentity = @user AND Email = @Email
				END
			END
		END
	END

--#endregion

--#region [@MakeFail9MonthsRule]

	IF(ISNULL(@MakeFail9MonthsRule, 0) = 1)
	BEGIN
		IF EXISTS(SELECT TOP 1 CreateDate FROM MailingSystem.UserEmails WITH(NOLOCK) WHERE UserIdentity = @user AND Email = @Email)
		BEGIN
			UPDATE MailingSystem.UserEmails
			SET CreateDate = DATEADD(YEAR, -2, GETUTCDATE()), ConfirmationDate = DATEADD(YEAR, -1, GETUTCDATE())
				WHERE UserIdentity = @user AND Email = @Email
			
			UPDATE tblClients
			SET DateOfAdded = DATEADD(YEAR, -2, GETUTCDATE()) 
				WHERE UserID = @user
		END
	END

--#endregion

--#region [@MakeConfirmedEmail]
	
	IF(ISNULL(@MakeConfirmedEmail, 0) = 1)
	BEGIN
		IF NOT EXISTS(SELECT TOP 1 UserIdentity FROM MailingSystem.UserEmails WITH(NOLOCK) WHERE Email = @Email AND UserIdentity = @user)
		BEGIN
			EXEC MailingSystem.CreateUserEmail @UserId=@user,@Email=@Email,@CreateDate=@time
		END

		UPDATE MailingSystem.UserEmails
		SET ConfirmationDate = GETUTCDATE() WHERE UserIdentity = @user
	END

--#endregion

--#region [@MakeUnconfirmedClient]

	IF(ISNULL(@MakeUnconfirmedClient, 0) = 1)
	BEGIN
		IF EXISTS(SELECT TOP 1 Email FROM MailingSystem.UserEmails WITH(NOLOCK) WHERE Email = @Email AND UserIdentity = @user)
		BEGIN
			EXEC ElectronicMail.[UnconfirmedInfo.Save] @UserId=@user,@TimeStamp=@time
			UPDATE MailingSystem.UserEmails
			SET ConfirmationDate = NULL
				WHERE UserIdentity = @user AND Email = @Email
		END
	END

--#endregion

--#region [@MakeOldClient]

	IF(ISNULL(@MakeOldClient, 0) = 1)
	BEGIN
		UPDATE tblClients
		SET DateOfAdded = DATEADD(MONTH, -3, GETUTCDATE()) WHERE UserId = (SELECT UserId FROM #users WHERE @i = idx)
	END

--#endregion	

--#region [@MakeNewClient]

	IF(ISNULL(@MakeNewClient, 0) = 1)
	BEGIN
		UPDATE tblClients
		SET DateOfAdded = DATEADD(DAY, -1, GETUTCDATE()) WHERE UserId = (SELECT UserId FROM #users WHERE @i = idx)
	END

--#endregion

--#region [PowerShell]

	DECLARE @Cmd VARCHAR(5000) = N'powershell.exe -NoProfile -NonInteractive -NoLogo -File "c:\app\digesttester\tools\digestsendlauncher.ps1" -AppDir "c:\App\digesttester" -ClientUserId ' + 
		CAST((SELECT UserId FROM tblClients WITH(NOLOCK) WHERE UserId = (SELECT UserId FROM #users WHERE idx = @i)) AS VARCHAR(1000))
	
	EXEC xp_cmdshell @Cmd;

--#endregion

  SET @r = NULL   
  SET @i = @i + 1
END

--#region [VerificationQuery]

SELECT DISTINCT u.*, c.EmailAddress, e.Confirmed AS EmailConfirmed, c.DateOfAdded AS RegistrationDate	
 FROM #users u WITH(NOLOCK)
	JOIN tblClients c WITH(NOLOCK) ON c.UserID = u.UserId 
	JOIN MailingSystem.UserEmails e WITH(NOLOCK) ON e.UserIdentity = u.UserId
		ORDER BY u.idx ASC

--#endregion