function ButtonTaskDelete({task, handleDelete}) {
  return (
    <button className="absolute top-1 right-1 w-4 h-4 m-0 p-0 rounded-full flex items-center justify-center
                       bg-gray-500 hover:bg-red-500 hover:border-none  text-white focus:outline-none active:scale-125 active:bg-red-800 transition-transform duration-150"
                       onClick={() => handleDelete(task.id)}>
            
            <svg xmlns="http://www.w3.org/2000/svg" className="h-3 w-3" fill="none" viewBox="0 0 24 24"
                        stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="3"
                            d="M6 18L18 6M6 6l12 12" />
                    </svg>
      </button>
  )
}

export default ButtonTaskDelete