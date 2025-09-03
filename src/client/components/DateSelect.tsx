interface IDateSelectProps {
  onChange: (value: number) => void;
  defaultValue?: number;
}

export default function DateSelect(props: IDateSelectProps) {
  const dates = [
    { value: -10119788, label: "Rakatan Empire (27,000 BBY)" },
    { value: -7900, label: "Battle of Geonosis (22 BBY)" },
  ];

  return (
    <span>
      <label>Date</label>
      <select
        onChange={(e) => props.onChange(parseInt(e.target.value))}
        defaultValue={props.defaultValue}
      >
        {dates.map(({ value, label }) => (
          <option key={value} value={value}>
            {label}
          </option>
        ))}
      </select>
    </span>
  );
}
