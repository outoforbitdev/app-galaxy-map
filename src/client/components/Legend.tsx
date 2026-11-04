import { SystemGlyph } from "@outoforbitdev/galaxy-map";
import styles from "../app/page.module.css";
import { toCssColor } from "./mapColors";

/** A legend entry as returned by the map API. */
export interface ILegendEntry {
  id: string;
  label: string;
  color: string;
}

interface ILegendProps {
  entries: ILegendEntry[];
}

/** Lists each government on the map next to a glyph in its color. */
export function Legend(props: ILegendProps) {
  return (
    <ul className={styles.legend}>
      {props.entries.map((entry) => (
        <li key={entry.id}>
          <svg width={16} height={16} aria-hidden="true">
            <SystemGlyph x={8} y={8} color={toCssColor(entry.color)} />
          </svg>
          {entry.label}
        </li>
      ))}
    </ul>
  );
}
