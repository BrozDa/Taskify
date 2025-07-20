import { useEffect, useState } from "react";

import { prioritiesGetAll } from "../services/apiPriorities";
import { tagsGetAll } from "../services/apiTags";
import { tasksGetAll } from "../services/apiTasks";

import DashboardMenu from '../components/DashboardMenu';
import DashboardHeader from '../components/DashboardHeader';
import Task from "../components/Task";
import NewTask from "../components/NewTask";

function Dashboard() {
    const [tasks, setTasks] = useState([]);
    const [priorities, setPriorities] = useState([]);
    const [tags, setTags] = useState([]);

    useEffect(() => {
        getTasks();
        getPriorities();
        getTags();
    },[])

    
    const getPriorities = async() => {
        const priorities = await prioritiesGetAll();
        setPriorities(priorities);
    }
    const getTags = async() => {
        const tags = await tagsGetAll();
        setTags(tags);
    }
    const getTasks = async() => {
        const tasks = await tasksGetAll();
        setTasks(tasks);
    }
  return (
    <div className="flex min-h-screen w-screen bg-gray-100">
        <DashboardMenu />
        <div className="flex flex-col flex-1">
            <DashboardHeader />
            <div className="flex flex-shrink-0 flex-wrap justify-start gap-6 p-4">
                <NewTask id="a" priorities={priorities} tags={tags}/>
                {tasks && tasks.map(t => (<Task key={t.id} task={t} />))}
            </div>
        </div>
    </div>
    
    
  )
}

export default Dashboard