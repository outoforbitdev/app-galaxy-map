interface IDateSelectProps {
  onChange: (value: number) => void;
  defaultValue?: number;
}

export default function DateSelect(props: IDateSelectProps) {
  const dates = [
    { value: -25000, label: "25000 BBY" },
    { value: -25, label: "25 BBY" },
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
