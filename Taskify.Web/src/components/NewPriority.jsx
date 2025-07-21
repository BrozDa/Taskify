import { useState } from 'react';

function NewPriority({ priorities, currentPriority, setNewPriority, whenAction="onHover", colors="bg-blue-700 hover:bg-blue-800 text-white"}) {

  const [dropDownOpen, setDropDownOpen] = useState(false);

  const handleOpen = () => {
    setDropDownOpen(true);
  }
  const handleClose = () => {
    setDropDownOpen(false);
  }
  const triggerProps =
    whenAction == "onHover" ? 
    {
      onMouseEnter: handleOpen,
      onMouseLeave: handleClose
    }
    : 
    {
      onDoubleClick: handleOpen,
      onClick:handleClose,
      onMouseLeave:handleClose
    };
  

  
  return (
      <div
        {...triggerProps}
        className="relative inline-block"
      >
        <button
          id="dropdownHoverButton"
          className={`w-32 font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex  items-center justify-center 
            ${colors}`}
          type="button"
        >
          {currentPriority ? currentPriority.name : "Priority"}
        </button>
        {dropDownOpen &&
          <div id="dropdown" className="absolute z-10 bg-white divide-y divide-gray-100 rounded-lg shadow-sm w-32 ">
            <ul className="py-2 text-sm text-gray-700 " aria-labelledby="dropdownDefaultButton">
              {priorities.map(p => (
                <li key={p.id}>
                  <span className="block px-2 py-1 hover:bg-gray-100 cursor-pointer" onClick={() => setNewPriority(p)}>
                    {p.name}</span>
                </li>
              ))}
            </ul>
          </div>
        }
      </div>
  )
}

export default NewPriority