export default function About() {
  return (
    <div>
      <h3>What this is?</h3>
      <p>
        This is a map of the systems in the Star Wars galaxy and the hyperlanes
        that connect them. It’s meant as a way to explore the galaxy.
      </p>
      <h3>Why did I make this?</h3>
      <p>
        As I read and watch Star Wars I enjoy trying to map out the path that
        our heroes are taking and the territory that the factions are fighting
        over. There’s no shortage of maps out there that all provide their own
        benefits, but there are none that I feel are feature-complete. This is
        my attempt to make a detailed map with all the features I always wanted.
      </p>
      <h3>What is the difference between the Legends and the Legends+ maps?</h3>
      <p>
        For the Legends map, I tried to stick as closely as possible to only
        data that I could confirm in sources from the Legends continuity. This
        means that of the over 5000 star systems in Legends, less than 30 of
        them are mapped and less than 2000 of them are on hyperlanes. For the
        Legends+ map I have added data that I think makes sense within the
        context of the Legends continuity. For example, the planet Lorell has
        been placed between Andalia and Zeltros and an extension of the Lorell
        Route has been added connecting all three systems.
      </p>
      <h3>How did I make this?</h3>
      <p>
        I gathered the initial data for this map from Star Wars: The Essential
        Atlas. Each of the maps in the Atlas, ranging from galactic to detailed
        sector-level maps, were imported into Gimip to determine the pixel
        locations of each system and calculate a galactic coordinate. I used
        Wookiepedia to compile all the hyperlanes in the galaxy, and tried to
        confirm each of those from the linked citation. The UI for this was
        built from the ground up as an SVG in React using Next.js. The server
        itself is Asp.Net Core with a SQL database.
      </p>
      <p>
        The UI for this was built from the ground up as an svg in React using
        Next.js.
      </p>
      <h3>What is next?</h3>
      <p>
        I have mapped over 2000 systems, but there are almost 3000 systems not
        yet mapped on either the Legends or Legends+ version. I will probably
        always be adding more as I find more info. The next piece of
        functionality is to show the government affiliations of each system at
        various timepoints, starting with the Clone Wars.
      </p>
    </div>
  );
}
