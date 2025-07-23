import { useState, useEffect } from 'react'
import { tasksAddTask } from '../services/apiTasks';

import TaskPriority from './TaskPriority';
import TaskText from './TaskText';
import TaskTag from './TaskTag';
import Button from './Button';
import TaskDate from './TaskDate';

function NewTask({priorities, tags, addNewTask}) {

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
  const handleRemoveTag = (tagId) => {
    setNewTaskTags(newTaskTags.filter(t => t.id !== tagId));
  }
  const handleNewTaskName = (value) => {
    value.length === 0 ? setNewTaskName("New Task Name") : setNewTaskName(value)

  }
  const handleNewTaskDescription = (value) => {
    value.length === 0 ? setNewTaskDescription("New Task Description") : setNewTaskDescription(value);
  }

  const handleSubmit = async() => {
    if(!isNewTaskValid()) {return;}

    const newTask = {
      name: newTaskName,
      description: newTaskDescription,
      dueDate: newTaskDueDate,
      priority: newPriority,
      tags: newTaskTags
    }
    const response = await tasksAddTask(newTask);
    if(errorMsg) setErrorMsg(null);
    addNewTask(response);

  }
  const isNewTaskValid = () => {
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
      return false;
    }
    return true;
  }
  return (
  <div className={`flex-col w-96 justify-around bg-blue-400 text-black m-4 rounded-xl min-h-48 p-4 shadow-lg  space-y-4`}>
    {errorMsg && 
      <div>
        <span className="font-semibold text-red-800">{errorMsg}</span>
      </div>
    }
    <div className="flex justify-between items-center">
      <span className={`px-2 py-2 bg-blue-400 text-black rounded text-sm font-semibold`}>
        <TaskPriority priorities={priorities} currentPriority = {newPriority} setNewPriority={setNewPriority}/>
      </span>
      <span className={`text-sm font-semibold bg-blue-400 text-black`}>
        <TaskDate currentDate={newTaskDueDate} setCurrentDate={setNewTaskDueDate} newTask={true}/>
      </span>
    </div>
      <TaskText text={newTaskName} setText={handleNewTaskName} newTask={true}/>
      <TaskText variant={"description"} text={newTaskDescription} setText={handleNewTaskDescription} newTask={true} allowEmpty={true}/>

    {/* Tags */}
    <div className="flex flex-wrap items-center justify-center gap-2">
      {newTaskTags.map(t => (
        <span
          key={t.name}
          className="px-2 py-1 bg-white/80 hover:bg-white/40 rounded-full text-xs font-medium"
          onDoubleClick={() => handleRemoveTag(t.id)}
        >
          #{t.name}
        </span>
      ))}
      <TaskTag tags={usableTags} setNewTag={handleAddTag}/>
    </div>
    <div className="flex justify-center items-center">
      <Button text={"Add new Task"} action={handleSubmit}/>
    </div>
  </div>
);
}

export default NewTask