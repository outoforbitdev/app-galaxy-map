import type { Metadata } from "next";
import localFont from "next/font/local";
import "./theme-colors.css";
import "./themes.css";
import "./globals.css";
import styles from "./page.module.css";
import { NavBar, NavLink } from "@/components/oodreact";
import { NavDropdown } from "@/components/oodreact/NavDropdown";

const geistSans = localFont({
  src: "./fonts/GeistVF.woff",
  variable: "--font-geist-sans",
  weight: "100 900",
});
const geistMono = localFont({
  src: "./fonts/GeistMonoVF.woff",
  variable: "--font-geist-mono",
  weight: "100 900",
});

export const metadata: Metadata = {
  title: "Star Wars Galactic Map",
  description: "Interactive map of the Star Wars Galaxy",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`${geistSans.variable} ${geistMono.variable}`}>
        <header className={styles.header}>
          <h1>Star Wars Galactic Map</h1>
          <NavBar home="/" homeLabel="Galactic Map" className={styles.navbar}>
            <NavDropdown label="Maps" className={styles.navbar_item}>
              <NavLink to="/map-legends" className={styles.navbar_dropdown}>Legends</NavLink>
              <NavLink to="/map-legends-next" className={styles.navbar_dropdown}>Legends+</NavLink>
            </NavDropdown>
            <NavLink to="/about" className={styles.navbar_item}>About</NavLink>
            <NavLink to="/resources" className={styles.navbar_item}>Resources</NavLink>
          </NavBar>
        </header>
        <div className={styles.content_row}>
          <div className={styles.content_container}>{children}</div>
        </div>
      </body>
    </html>
  );
}
