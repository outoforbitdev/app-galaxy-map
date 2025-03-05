TRUNCATE instances CASCADE;
COPY instances FROM '/data/db/instances.csv' DELIMITER ',' HEADER;
COPY solar_systems (instance_id,id,name,x,y,sector,region,focus)
    FROM '/data/db/solar-systems.csv' DELIMITER ',' HEADER;
COPY spacelanes (instance_id,name,origin_id,destination_id,focus)
    FROM '/data/db/spacelanes.csv' DELIMITER ',' HEADER;
COPY planets (instance_id,id,name,system_id)
    FROM '/data/db/planets.csv' DELIMITER ',' HEADER;
COPY governments (instance_id,id,name,color_string)
    FROM '/data/db/governments.csv' DELIMITER ',' HEADER;
COPY planet_governments (instance_id,planet_id,government_id,relationship_string)
    FROM '/data/db/planet-governments.csv' DELIMITER ',' HEADER;
COPY government_governments (instance_id,child_government_id,parent_government_id,relationship_string)
    FROM '/data/db/government-governments.csv' DELIMITER ',' HEADER;
