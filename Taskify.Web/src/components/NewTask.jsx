import React, { useState, useEffect } from 'react'
import NewTaskPriority from './NewTaskPriority';
import NewTaskDatePicker from './NewTaskDatePicker';
import NewTaskText from './NewTaskText';
import NewTaskAddTag from './NewTaskAddTag';
import Button from './Button';
function NewTask({priorities, tags}) {

  const [newPriority, setNewPriority] = useState(null);
  const [newTaskDueDate, setNewTaskDueDate] = useState(null);
  const [newTaskName, setNewTaskName] = useState("New Task Name");
  const [newTaskDescription, setNewTaskDescription] = useState("New Task Description");
  const [newTaskTags, setNewTaskTags] = useState([]);
  const [usableTags, setUsableTags] = useState([]);
  const [errorMsg, setErrorMsg] = useState(null);

  useEffect(() => {
  setUsableTags([...tags]);
  } , [tags]);

  const handleAddTag = (newTag) => {
    setNewTaskTags([...newTaskTags, newTag])
    setUsableTags(usableTags.filter(t => t.name !== newTag.name));
  }
  const handleNewTaskName = (value) => {
    {console.log(value)}
    if(value.length === 0) setNewTaskName("New Task Name")
    else setNewTaskName(value)
  }
  const handleNewTaskDescription = () => {
    if(newTaskDescription.length === 0) setNewTaskDescription("New Task Description")
  }

  const handleSubmit = async() => {
    let error = "";

    if(!newPriority)
      error += "Priority, "
    if(!newTaskDueDate)
      error += "Due Date, "
    if(newTaskName === "New Task Name")
      error += "Task name"
    
    if(error !== "" ){
      error = "Missing required fields: " + 
        (error.charAt(error.length-2) === "," ? (error.slice(0,error.length-2)) : error);
      setErrorMsg(error);
      return;
    }
    console.log(newPriority)
    console.log(newTaskDueDate)
    console.log(newTaskName)
    console.log(newTaskDescription)
    console.log(newTaskTags)
  }
  return (
  <div className={`flex-col w-96 justify-around bg-blue-400 text-black m-4 rounded-xl min-h-48 p-4 shadow-lg  space-y-4`}>
    {errorMsg && 
      <div>
        <span>{errorMsg}</span>
      </div>
    }
    <div className="flex justify-between items-center">
      <span className={`px-2 py-2 bg-blue-400 text-black rounded text-sm font-semibold`}>
        <NewTaskPriority priorities={priorities} currentPriority = {newPriority} setNewPriority={setNewPriority}/>
      </span>
      <span className={`text-sm font-semibold bg-blue-400 text-black`}>
        <NewTaskDatePicker currentDate={newTaskDueDate} setCurrentDate={setNewTaskDueDate}/>
      </span>
    </div>
    <div>
      <NewTaskText currentText={newTaskName} setCurrentText={handleNewTaskName}/>
    </div>
    {/* Description */}
    <div>
      <NewTaskText currentText={newTaskDescription} setCurrentText={handleNewTaskDescription}/>
    </div>

    {/* Tags */}
    <div className="flex flex-wrap items-center justify-center gap-2">
      {newTaskTags.map(t => (
        <span
          key={t.name}
          className="px-2 py-1 bg-white/80 hover:bg-white/40 rounded-full text-xs font-medium"
        >
          #{t.name}
        </span>
      ))}
      <NewTaskAddTag tags={usableTags} setNewTag={handleAddTag}/>
    </div>
    <div className="flex justify-center items-center">
      <Button text={"Submit"} action={handleSubmit}/>
    </div>
  </div>
);
}

export default NewTask