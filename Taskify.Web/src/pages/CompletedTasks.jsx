import { useEffect, useState, useRef } from "react";

import { prioritiesGetAll } from "../services/apiPriorities";
import { tagsGetAll } from "../services/apiTags";

import { tasksGetCompleted } from "../services/apiTasks";


import DashboardMenu from '../components/DashboardMenu';
import DashboardHeader from '../components/DashboardHeader';
import CompletedTask from "../components/CompletedTask";
import Loading from "../components/Loading";

function Dashboard() {
    const [tasks, setTasks] = useState([]);
    const [priorities, setPriorities] = useState([]);
    const [tags, setTags] = useState([]);

    //Due to strict mode double fetching
    const hasFetchedTasks = useRef(false);
    const hasFetchedPriorities = useRef(false);
    const hasFetchedTags = useRef(false);

    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async() => {
            try{
                await getCompletedTasks();
                await getPriorities();
                await getTags();
            }
            catch (error){
                console.error(error);
                setError("Unexpected error while fetching data");
            }
            finally{
                setLoading(false);
            }  
        };
        fetchData();
    },[])

    const getCompletedTasks = async() => {
        //Due to strict mode double fetching
        if (hasFetchedTasks.current) return;
        hasFetchedTasks.current = true;

        if(tasks.length === 0){
            const fetchedTasks = await tasksGetCompleted();
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
    
  return (
    <div className="flex min-h-screen w-screen bg-gray-100">
        <DashboardMenu />
        <div className="flex flex-col">
            <DashboardHeader />
            {loading 
            ?
            <Loading/>
            :
            <div className="flex flex-wrap justify-start items-center gap-6 p-4">
                {error
                ?
                <div>
                    <p>{error}</p>
                </div>
                :
                <>
                {tasks && tasks.map(t => (<CompletedTask key={t.id} task={t}/>))}
                </>
                }  
            </div>
            }
        </div>
    </div>
  )
}

export default Dashboard