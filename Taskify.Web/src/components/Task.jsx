import ButtonTaskDelete from './ButtonTaskDelete';

import { tagsDeleteTag } from '../services/apiTags';
import { useEffect, useState } from 'react';
import { tasksUpdateTags } from '../services/apiTasks';

import NewTag from './NewTag';

function Task({ task, allTags, handleDelete}) {

  const [tags, setTags] = useState(task.tags);
  const [usableTags, setUsableTags] = useState(allTags.filter(t => !task.tags.some(tag => tag.id === t.id)));


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

  return (

    <div className={`relative flex flex-col w-96 h-72 justify-around ${colors[task.priority.backgroundClass]} rounded-xl shadow-lg p-4 m-4`}>
      <div className=" absolute top-2 right-2 z-20" >
        <ButtonTaskDelete task={task} handleDelete={handleDelete} />
      </div>
      <div className="flex justify-between items-center mx-1 -mt-2">
        <span className={`px-2 py-2 ${priorityColors[task.priority.backgroundClass]} rounded text-sm font-semibold`}>
          {task.priority.name}
        </span>
        <span className={`text-sm font-semibold ${colors[task.priority.backgroundClass]}`}>{`Due: ${formattedDate}`}</span>

      </div>
      <div>
        <h2 className="text-2xl font-bold">{task.name}</h2>
      </div>
      {/* Description */}
      <div className="flex justify-center max-h-12 overflow-y-auto px-1">
        <p className="text-sm whitespace-normal break-words text-center max-w-full">{task.description}</p>
      </div>

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
        <NewTag tags={usableTags} setNewTag={handleAddTag}/>
      </div>
    </div>
  );
}

export default Task