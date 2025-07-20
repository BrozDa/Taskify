import React from 'react'
import { useState } from 'react';
function NewTaskAddTag({tags, setNewTag}) {

  const [dropDownOpen, setDropDownOpen] = useState(false);
    const handleMouseEnter = () => {
      setDropDownOpen(true);
    }
    const handleMouseLeave = () => {
      setDropDownOpen(false);
    }

  return (
      <div
        onMouseEnter={handleMouseEnter}
        onMouseLeave={handleMouseLeave}
        className="relative inline-block"
      >
        <span
          id="dropdownHoverButton"
          className="px-2 py-1 bg-blue-700 hover:bg-blue-800 text-white rounded-full text-xs font-medium"
        >
          #New Tag
        </span>
        {dropDownOpen &&
          <div id="dropdown" className="absolute z-10 bg-white divide-y divide-gray-100 rounded-lg shadow-sm w-24 dark:bg-gray-700 ">
            <ul className="py-0.5 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownDefaultButton">
              {tags.map(t => (
                <li key={t.id}>
                  <span className="block px-2 py-1 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white cursor-pointer" onClick={() => setNewTag(t)}>{t.name}</span>
                </li>
              ))}
            </ul>
          </div>
        }
      </div>
  )

}

export default NewTaskAddTag