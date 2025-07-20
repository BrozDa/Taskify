import { useEffect, useState, useRef } from "react";

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

    //Due to strict mode double fetching
    const hasFetchedTasks = useRef(false);
    const hasFetchedPriorities = useRef(false);
    const hasFetchedTags = useRef(false);

    useEffect(() => {
        getTasks();
        getPriorities();
        getTags();
    },[])

    const getTasks = async() => {
        //Due to strict mode double fetching
        if (hasFetchedTasks.current) return;
        hasFetchedTasks.current = true;

        if(tasks.length === 0){
            const fetchedTasks = await tasksGetAll();
            setTasks(fetchedTasks);
        }
    }

    const getPriorities = async() => {
        //Due to strict mode double fetching
        if (hasFetchedPriorities.current) return;
        hasFetchedPriorities.current = true;

        if(priorities.length === 0){
            const fetchedPriorities = await prioritiesGetAll();
            setPriorities(fetchedPriorities);     
        }
    }
    const getTags = async() => {
        //Due to strict mode double fetching
        if (hasFetchedTags.current) return;
        hasFetchedTags.current = true;

        if(tags.length === 0){
            const fetchedTags = await tagsGetAll();
            setTags(fetchedTags);
        }
    }
    const handleAddNewTask = (newTask) =>
    {
        setTasks([newTask,...tasks])
    }
    
  return (
    <div className="flex min-h-screen w-screen bg-gray-100">
        <DashboardMenu />
        <div className="flex flex-col flex-1">
            <DashboardHeader />
            <div className="flex flex-shrink-0 flex-wrap justify-start gap-6 p-4">
                <NewTask id="a" priorities={priorities} tags={tags} addNewTask={handleAddNewTask}/>
                {tasks && tasks.map(t => (<Task key={t.id} task={t} />))}
            </div>
        </div>
    </div>
    
    
  )
}

export default Dashboard