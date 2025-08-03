interface IDateSelectProps {
  onChange: (value: number) => void;
  defaultValue?: number;
}

export default function DateSelect(props: IDateSelectProps) {
  const dates = [
    { value: -10672000, label: "29000 BBY" },
    { value: -9936000, label: "27000 BBY" },
    { value: -8832000, label: "24000 BBY" },
    { value: 0, label: "0 BBY" },
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
