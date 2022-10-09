
DECLARE @IdLote int = 1016;
DECLARE @Fecha datetime = '20221003';

-- Hours setting
DECLARE @ListHours TABLE(Hour INT);
INSERT INTO @ListHours (Hour)
VALUES(0), (1), (2), (3), (4), (5), (6), (7), (8), (9), (10), (11), (12), (13), (14), (15), (16), (17), (18), (19), (20), (21), (22), (23); 

WITH Balance AS (
	SELECT 
		ds.Indice, 
		m.Estado,		
		DATEPART(HOUR, m.Fecha) as Hora
	FROM EspacioDelimitado ds 
	JOIN Monitoreo m on m.IdEspacioDelimitado = ds.IdEspacioDelimitado
	WHERE ds.IdLote = @IdLote 
	and m.Fecha BETWEEN @Fecha + ' 00:00:00' AND @Fecha + ' 23:59:59'
),
Ocupaciones AS (
	SELECT 
		Indice,
		MAX(CAST(Estado AS INT)) as Estado,  
		Hora 
	FROM Balance
	GROUP BY Hora, Indice
)
SELECT 
	H.Hour,
	CASE
		WHEN SUM(Estado) IS NULL THEN 0
		ELSE SUM(Estado)
	END as Ocupaciones	
FROM @ListHours h
LEFT JOIN Ocupaciones oc on oc.Hora = H.Hour
GROUP BY H.Hour
ORDER BY H.Hour
