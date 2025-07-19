import { useAuth } from "../context/AuthContext";
import DashboardMenu from '../components/DashboardMenu';
import DashboardHeader from '../components/DashboardHeader';
import { useEffect, useState } from "react";
import { prioritiesGetAll } from "../services/apiPriorities";
import { tagsGetAll } from "../services/apiTags";
import { tasksGetAll } from "../services/apiTasks";
import Task from "../components/Task";

function Dashboard() {
    const [tasks, setTasks] = useState([]);

    useEffect(() => {handleDataLoad();},[])

    const handleDataLoad = async() => {
        const tasks = await tasksGetAll();
        setTasks(tasks);
    }
  return (
    <div className="flex min-h-screen w-screen bg-gray-100">
        <DashboardMenu />
        <div className="flex flex-col flex-1">
            <DashboardHeader />
            <div className="flex flex-shrink-0 flex-wrap justify-start gap-6 p-4">
                {tasks && tasks.map(t => (<Task key={t.id} task={t} />))}
            </div>
        </div>
    </div>
    
    
  )
}

export default Dashboard