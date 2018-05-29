#create land of nephi unit
INSERT INTO political_unit VALUES('Land of Nephi','ne', 1.5); 

#Map
INSERT INTO boundary VALUES(6, ST_GeomFromText('POLYGON((4 0,4 7,0 7,0 13,1 13,1 18,6 18,6 20,5 20,5 22,12 22,12 20,10 20,10 18,16 18, 16 14, 18 14, 18 9, 14 9, 14 6, 15 6, 15 0, 4 0))'),'ne');
#river
INSERT INTO boundary VALUES(7,ST_GeomFromText('linestring(5 18, 8 14, 8 13, 7 12, 7 11, 8 7, 8 5)'), 'ne');

#cities of the Land of Nephi
INSERT INTO city VALUES('Bountiful', ST_GeomFromText('POINT(10 17)'),'ne'); 
INSERT INTO city VALUES('Mulek', ST_GeomFromText('POINT(12 16)'),'ne'); 
INSERT INTO city VALUES('Zarahemla', ST_GeomFromText('POINT(6 14)'),'ne'); 
INSERT INTO city VALUES('Ammonihah', ST_GeomFromText('POINT(3 13)'),'ne'); 
INSERT INTO city VALUES('Antiparah', ST_GeomFromText('POINT(4 12)'),'ne'); 
INSERT INTO city VALUES('Gideon', ST_GeomFromText('POINT(9 13)'),'ne'); 
INSERT INTO city VALUES('Jershon', ST_GeomFromText('POINT(14 12)'),'ne'); 
INSERT INTO city VALUES('Nephihah', ST_GeomFromText('POINT(13 10)'),'ne'); 
INSERT INTO city VALUES('Manti', ST_GeomFromText('POINT(6 10)'),'ne'); 
INSERT INTO city VALUES('Middoni', ST_GeomFromText('POINT(6 4)'),'ne'); 
INSERT INTO city VALUES('Ishmael', ST_GeomFromText('POINT(5 2)'),'ne'); 
INSERT INTO city VALUES('Midian', ST_GeomFromText('POINT(7 3)'),'ne'); 
INSERT INTO city VALUES('Shilom', ST_GeomFromText('POINT(10 2)'),'ne'); 
INSERT INTO city VALUES('Jerusalem', ST_GeomFromText('POINT(10 2)'),'ne'); 
INSERT INTO city VALUES('Shemlon', ST_GeomFromText('POINT(12 3)'),'ne'); 
INSERT INTO city VALUES('Amulon', ST_GeomFromText('POINT(14 5)'),'ne'); 
INSERT INTO city VALUES('Helam', ST_GeomFromText('POINT(11 5)'),'ne');
INSERT INTO city VALUES('Moroni', ST_GeomFromText('POINT(16 11)'),'ne');  

#What is the aread of the land_of_nephi?
SELECT ST_AREA(boundpath) AS 'Area'
FROM political_unit
JOIN boundary on political_unit.Unitcode = boundary.Unitcode
WHERE Unitname = 'Land of Nephi'AND  Boundid = '6';

#what is the distance from Bountiful to Zarahemla?
SELECT ST_DISTANCE(orig.cityloc, dest.cityloc) AS 'Distance'
FROM city orig JOIN city dest
WHERE orig.cityname = 'Bountiful' AND dest.cityname = 'Zarahemla';

#What city is the farthest east?
SELECT cityname, cityloc FROM city WHERE ST_X(cityloc) = (
 SELECT MAX(ST_X(cityloc)) FROM city WHERE Unitcode = 'ne');

#What city is the farthest north?
SELECT cityname FROM city WHERE ST_Y(cityloc) = (SELECT MAX(ST_Y(cityloc)) 
FROM city WHERE Unitcode = 'ne');


#what is the length of the river sidon?
select ST_Length(Boundpath) from boundary where Boundid = '7';

#what is the farthest city from Nephihah?
SELECT dest.cityname
FROM city orig JOIN city dest
WHERE orig.cityname = 'Nephihah' AND dest.cityName <> 'Nephihah' AND dest.Unitcode = 'ne'
ORDER BY ST_DISTANCE(orig.Cityloc, dest.Cityloc) DESC
LIMIT 1;

#What are the three closest cities to Manti?
SELECT dest.cityname
FROM city orig JOIN city dest
WHERE orig.cityname = 'Manti' AND dest.cityname <> 'Manti' AND dest.Unitcode = 'ne'
ORDER BY ST_DISTANCE(orig.Cityloc, dest.cityloc)
LIMIT 3;