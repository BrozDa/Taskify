function LoginInput({ label, id, type = 'text', value,onChange,required}) {
  return (
    <div>
      <label htmlFor={id} className="block mb-1 text-sm font-medium text-blue-400">
        {label}
      </label>
      <input
        id={id}
        type={type}
        className={`
          w-full px-3 py-2 text-sm border rounded-lg shadow-sm focus:outline-none
          bg-gray-50 border-gray-300 text-gray-900 focus:ring-2 focus:ring-blue-500 focus:border-blue-500
        `}
        value={value}
        onChange={onChange}
        required={required}
        />
    </div>
  );
}

export default LoginInput