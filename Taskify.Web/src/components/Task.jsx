import ButtonTaskDelete from './ButtonTaskDelete';

import { useEffect, useState } from 'react';
import { tasksUpdatePriority, 
  tasksUpdateTags, tasksUpdateName , 
  tasksUpdateDescription, 
  tasksUpdateDate, 
       } from '../services/apiTasks';

import TaskPriority from './TaskPriority';
import TaskText from './TaskText';
import TaskTag from './TaskTag';
import TaskDate from './TaskDate';
import Button from './Button';

function Task({ task, allTags, allPriorities, handleDelete, handleComplete}) {

  const [tags, setTags] = useState(task.tags);
  const [usableTags, setUsableTags] = useState(allTags.filter(t => !task.tags.some(tag => tag.id === t.id)));
  const [priority, setPriority] = useState(task.priority);
  const [name, setName] = useState(task.name);
  const [description, setDescription] = useState(task.description);
  const [dueDate, setDueDate] = useState(task.dueDate);

  useEffect(() => { }, [tags]);

  const date = new Date(task.dueDate);

  const colors = {
    red: "bg-red-200 text-red-800",
    orange: "bg-orange-200 text-orange-800",
    yellow: "bg-yellow-200 text-yellow-800",
    green: "bg-green-200 text-green-800",
  }
  const priorityColors = {
    red: "bg-red-400 hover:bg-red-400/50",
    orange: "bg-orange-400 hover:bg-orange-400/50",
    yellow: "bg-yellow-400 hover:bg-yellow-400/50",
    green: "bg-green-400 hover:bg-green-400/50",
  }

  const formattedDate = new Intl.DateTimeFormat('cs-CZ', {
    dateStyle: 'medium',
  }).format(date);

  const handleDeleteTag = async (tagId) => {
    const updatedTags = tags.filter(t => t.id !== tagId);
    const result = await tasksUpdateTags(task.id, updatedTags.map(t => t.id));
    setTags(updatedTags);
  }
  const handleAddTag = async(newTag) => {

    const updatedTags = [...tags,newTag];
    const result = await tasksUpdateTags(task.id, updatedTags.map(t => t.id))
    setTags(updatedTags);
    setUsableTags(usableTags.filter(t => t.id !== newTag.id));
  }
  const setNewPriority = async (priority) => {
    const result = await tasksUpdatePriority(task.id, priority.id);
    setPriority(priority);
  }
  const handleUpdateName = async(text) => {
    const result = await tasksUpdateName(task.id, text);
    setName(text);
  }
  const handleUpdateDescription = async(text) => {
    const result = await tasksUpdateDescription(task.id, text);
    setDescription(text);
  }
  const handleUpdateDate = async(text) => {
    const result = await tasksUpdateDate(task.id, text);
    setDueDate(text);
  }

  return (
    <div className={`flex flex-1 flex-col max-w-xl justify-around ${colors[priority.backgroundClass]} text-black m-4 rounded-xl min-h-48 p-4 shadow-lg  space-y-4`}>
      <div className=" absolute top-2 right-2 z-20" >
        <ButtonTaskDelete task={task} handleDelete={() => handleDelete(task.id)} />
      </div>
      <div className="flex justify-between items-center mx-1 -mt-2">

        <TaskPriority priorities={allPriorities} currentPriority = {priority} setNewPriority={setNewPriority} whenAction="onDoubleClick" 
        colors={priorityColors[priority.backgroundClass]}/>

        <TaskDate currentDate={new Date(dueDate)} setCurrentDate={handleUpdateDate} colors={colors[priority.backgroundClass]}/>

      </div>
      <TaskText variant={"name"} text={name} setText={handleUpdateName}/>
      <TaskText variant={"description"} text={description} setText={handleUpdateDescription} allowEmpty={true}/>

      {/* Tags */}
      <div className="flex flex-wrap gap-2 justify-center">
        {tags.map((t) => (
          <span
            key={t.id}
            className="px-2 py-0.5 bg-white/80 hover:bg-white/40 rounded-full text-xs font-medium "
            onDoubleClick={() => handleDeleteTag(t.id)}
          >
            #{t.name}
          </span>
        ))}
        <TaskTag tags={usableTags} setNewTag={handleAddTag}/>
      </div>
      <Button 
        text={"Complete"} 
        action={()=>handleComplete(task.id)} 
        colors={priorityColors[priority.backgroundClass]}/>
    </div>
  );
}

export default Task