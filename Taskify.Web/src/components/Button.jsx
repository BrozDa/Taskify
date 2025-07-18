import clsx from 'clsx'

function Button({type,text,action}) {
  return (
    <button
    type={type}
    onClick={action}
      className={clsx(
        'w-full px-4 py-2 text-white text-sm font-medium rounded-lg',
        'bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2',
        'dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-500'
      )}
    >
    {text}
    </button>
  );
}

export default Button