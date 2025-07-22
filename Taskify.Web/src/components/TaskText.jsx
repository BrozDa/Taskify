import { useState, useRef, useEffect } from 'react'

function TaskText({ variant, text, setText, newTask = false, allowEmpty=true}) {

  const [editText, setEditText] = useState(text);
  const [isEditing, setIsEditing] = useState(newTask);

  const clickOutsideRef = useRef(null);
  const inputRef = useRef();
  
  useEffect(() => {
    if((isEditing || newTask) && inputRef.current){
      inputRef.current.focus();
    }
  },[isEditing]);

  const handleDoubleClick = () =>{
    if(newTask) return;
    else setIsEditing(true);
  }
  const handleKeyDown = (e) => {
    if(!isEditing) return;
    if(e.code == "Enter" || e.code == "NumpadEnter"){
      finishEdit();
    }

  }
  const finishEdit = () =>{
    if(!allowEmpty){
      editText.length > 0 ? setText(editText) : setEditText("Required field");
    }
    else{
      setText(editText);
    }
    setIsEditing(false);
  }
  return (
    <div
      onDoubleClick={() => handleDoubleClick()}
      onKeyDown={(e) => handleKeyDown(e)}
      className="flex justify-center max-h-12 overflow-y-auto px-1">
      
      {(isEditing || newTask)
        ?
          <input 
          ref = {inputRef} 
          type="text" 
          id="new-task-text" 
          placeholder={text}
          onChange={(e) => setEditText(e.target.value)}
          onBlur={finishEdit}
          className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600  dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500 text-center"
          />
        :
        variant === "name"
          ?
          <h2 className="text-2xl font-bold">{text}</h2>
          :
          <p className="text-sm whitespace-normal break-words text-center max-w-full">{text}</p>
          
      }
      
    </div>
  )

}

export default TaskText