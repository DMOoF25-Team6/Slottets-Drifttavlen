CREATE ALGORITHM=UNDEFINED 
DEFINER=`root`@`localhost` 
SQL SECURITY DEFINER 
VIEW `medicinestatusview` AS 
SELECT 
  r.Id AS ResidentId,
  r.Initials AS Initials,
  m.MedicineName AS MedicineName,
  m.Timestamp AS Timestamp,
  m.Given AS Given
FROM resident r
JOIN medicinerecord m ON r.Id = m.ResidentId
WHERE m.Timestamp >= (NOW() - INTERVAL 24 HOUR);



CREATE VIEW painkillerstatusview AS
SELECT 
  r.Id AS ResidentId,
  r.Initials AS Initials,
  p.Type AS Type,
  p.GivenAt AS GivenAt,
  p.NextAllowedTime AS NextAllowedTime
FROM resident r
JOIN painkillerrecord p ON r.Id = p.ResidentId;
