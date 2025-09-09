import styles from "../page.module.css";

export default function About() {
  return (
    <div className={styles.text_container}>
      <h3>What this is?</h3>
      <p>
        This is a map of the systems in the Star Wars galaxy and the hyperlanes
        that connect them. It is designed as a way to explore the galaxy and
        better understand how everything fits together.
      </p>
      <h3>Why did I make this?</h3>
      <p>
        As I read and watch Star Wars, I enjoy trying to map out the paths our
        heroes take and the territories various factions control. While there
        are plenty of maps out there, each with their own strengths, I have
        never found one that feels feature-complete. This is my attempt to build
        the detailed map I have always wanted.
      </p>
      <h3>What is the difference between the Legends and the Legends+ maps?</h3>
      <p>
        For the Legends map, I have stuck as closely as possible to data
        confirmed by sources from the Legends continuity. That means out of the
        5,000+ star systems in Legends, fewer than 3,000 are actually mapped,
        and under 2,000 are connected by hyperlanes.
      </p>
      <p>
        The Legends+ map includes additional data that I believe fits within the
        context of Legends, even if it has not been explicitly confirmed. For
        example, I have placed the planet Lorell between Andalia and Zeltros,
        and extended the Lorell Route to connect all three systems.
      </p>
      <h3>How did I make this?</h3>
      <p>
        I started by gathering data from Star Wars: The Essential Atlas. I
        imported each map—from galactic-scale to detailed sector-level—into GIMP
        to determine the pixel coordinates of each system and calculate their
        galactic positions.
      </p>
      <p>
        I used Wookieepedia to compile information on the galaxy’s hyperlanes,
        confirming each one using the cited sources whenever possible.
      </p>
      <p>
        The UI was built from the ground up as an SVG in React using Next.js.
        The server runs on ASP.NET Core and connects to a SQL database.
      </p>
      <p>
        The UI for this was built from the ground up as an svg in React using
        Next.js.
      </p>
      <h3>What is next?</h3>
      <p>
        So far, I have mapped over 2,000 systems, but nearly 3,000 more remain
        unmapped in both the Legends and Legends+ versions. I will probably
        always be adding more as I discover new information. The next major
        feature will be the ability to view the government affiliations of each
        system at various timepoints—starting with the Clone Wars.
      </p>
    </div>
  );
}
