import type { Metadata } from "next";
import localFont from "next/font/local";
import "./globals.css";
import styles from "./page.module.css";
import NavBar from "../components/NavBar";

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
      <body
        className={`${geistSans.variable} ${geistMono.variable} ood-primary`}
      >
        <header className={styles.header}>
          <NavBar />
        </header>
        <div className={styles.content_row}>
          <div className={styles.content_container}>{children}</div>
        </div>
      </body>
    </html>
  );
}
