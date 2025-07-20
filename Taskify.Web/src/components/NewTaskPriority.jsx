import React, { useEffect } from 'react'
import { useState } from 'react';

function NewTaskPriority({ priorities, currentPriority, setNewPriority }) {
  
  const [dropDownOpen, setDropDownOpen] = useState(false);
  const handleMouseEnter = () => {
    setDropDownOpen(true);
  }
  const handleMouseLeave = () => {
    setDropDownOpen(false);
  }

  return (
    <>
      <div 
  onMouseEnter={handleMouseEnter}
  onMouseLeave={handleMouseLeave}
  className="relative inline-block"
>
  <button
    id="dropdownHoverButton"
    className="w-32 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex  items-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 justify-center"
    type="button"
  >
    {currentPriority? currentPriority.name : "Priority"}
  </button>
  {dropDownOpen &&
    <div id="dropdown" className="absolute z-10 bg-white divide-y divide-gray-100 rounded-lg shadow-sm w-32 dark:bg-gray-700 ">
      <ul className="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownDefaultButton">
        {priorities.map(p => (
            <li key={p.id}>
              <span className="block px-2 py-1 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white cursor-pointer" onClick={() => setNewPriority(p)}>{p.name}</span>
            </li>
        ))}
      </ul>
    </div>
  }
</div>
      
    </>
  )
}

export default NewTaskPriority