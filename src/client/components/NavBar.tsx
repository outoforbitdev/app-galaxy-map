"use client";

import styles from "../app/page.module.css";
import {
  NavBar as GenericNavBar,
  NavDropdown,
  NavLink,
} from "@outoforbitdev/ood-react";

export default function NavBar() {
  return (
    <GenericNavBar
      home="/"
      homeLabel="Star Wars Galactic Map"
      className="ood-accent-block"
    >
      <NavDropdown label="Maps" className={styles.navbar_item}>
        <NavLink to="/map-legends" className={styles.navbar_dropdown}>
          Legends
        </NavLink>
        <NavLink to="/map-legends-plus" className={styles.navbar_dropdown}>
          Legends+
        </NavLink>
      </NavDropdown>
      <NavLink to="/about" className={styles.navbar_item}>
        About
      </NavLink>
      <NavLink to="/resources" className={styles.navbar_item}>
        Resources
      </NavLink>
    </GenericNavBar>
  );
}
