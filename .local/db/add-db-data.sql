TRUNCATE solar_systems CASCADE;
TRUNCATE governments CASCADE;
COPY solar_systems FROM '/data/db/solar-systems.csv' DELIMITER ',' HEADER;
COPY spacelanes FROM '/data/db/spacelanes.csv' DELIMITER ',' HEADER;
COPY planets FROM '/data/db/planets.csv' DELIMITER ',' HEADER;
COPY governments FROM '/data/db/governments.csv' DELIMITER ',' HEADER;
COPY planet_governments FROM '/data/db/planet-governments.csv' DELIMITER ',' HEADER;
COPY government_governments FROM '/data/db/government-governments.csv' DELIMITER ',' HEADER;
