/**
 * Maps the color names returned by the map API (the backend `MapColor` enum)
 * to ood-react theme color variables, so the map follows the active theme.
 */
const THEME_COLORS: Record<string, string> = {
  Gray: "var(--ood-color-gray)",
  Red: "var(--ood-color-red)",
  Blue: "var(--ood-color-blue)",
  Green: "var(--ood-color-green)",
  Yellow: "var(--ood-color-yellow)",
  Magenta: "var(--ood-color-magenta)",
  Aqua: "var(--ood-color-cyan)",
};

/** Converts an API color name to a CSS color, falling back to gray. */
export function toCssColor(name: string | undefined): string {
  return THEME_COLORS[name ?? ""] ?? THEME_COLORS.Gray;
}
