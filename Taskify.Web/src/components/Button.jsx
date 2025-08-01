function Button({type,text,action, colors="bg-blue-600 hover:bg-blue-700 text-white dark:bg-blue-500"}) {
  return (
    <button
    type={type}
    onClick={action}
      className=
        {`w-full px-4 py-2  text-sm font-bold rounded-lg
        ${colors} `}
    >
    {text}
    </button>
  );
}

export default Button