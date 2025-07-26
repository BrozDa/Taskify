import { useEffect, useState, useRef } from "react";

import { prioritiesGetAll } from "../services/apiPriorities";
import { tagsGetAll } from "../services/apiTags";
import {
    tasksGetPending,
    tasksDeleteTask,
    tasksCompleteTask
} from "../services/apiTasks";

import Task from "../components/Task";
import NewTask from "../components/NewTask";
import Loading from "../components/Loading";

function Dashboard() {
    const [tasks, setTasks] = useState([]);
    const [priorities, setPriorities] = useState([]);
    const [tags, setTags] = useState([]);

    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);

    //Due to strict mode double fetching
    const hasFetchedTasks = useRef(false);
    const hasFetchedPriorities = useRef(false);
    const hasFetchedTags = useRef(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                await getPendingTasks();
                await getPriorities();
                await getTags();
            }
            catch (error) {
                console.error(error);
                setError("Unexpected error while fetching data");
            }
            finally {
                setLoading(false);
            }
        };
        fetchData();
    }, [])
    useEffect(()=>{},[tags])

    const handleDeleteTask = async (taskId) => {
        await tasksDeleteTask(taskId);
        setTasks(tasks.filter(t => t.id !== taskId));
    }
    const handleCompleteTask = async (taskId) => {
        await tasksCompleteTask(taskId);
        setTasks(tasks.filter(t => t.id !== taskId));
    }

    const getPendingTasks = async () => {
        //Due to strict mode double fetching
        if (hasFetchedTasks.current) return;

        hasFetchedTasks.current = true;
        const fetchedTasks = await tasksGetPending();
        setTasks(fetchedTasks);
    }

    const getPriorities = async () => {
        //Due to strict mode double fetching
        if (hasFetchedPriorities.current) return;

        hasFetchedPriorities.current = true;
        const fetchedPriorities = await prioritiesGetAll();
        setPriorities(fetchedPriorities);
    }
    const getTags = async () => {
        //Due to strict mode double fetching
        if (hasFetchedTags.current) return;

        hasFetchedTags.current = true;
        const fetchedTags = await tagsGetAll();
        setTags(fetchedTags);
    }
    const handleSetTask = (newTask) => {
        setTasks([newTask, ...tasks]);
    }



    return (
        <div className="py-12 pl-40 sm:ml-7">      
            <div className="flex flex-col flex-1">
                {loading
                    ?
                    <Loading />
                    :
                    <div className="grid grid-cols-[repeat(auto-fill,minmax(360px,1fr))] gap-4">
                        {error
                            ?
                            <div>
                                <p>{error}</p>
                            </div>
                            :
                            <>
                                <NewTask id="newTask" priorities={priorities} tags={tags} setTags={setTags} addNewTask={handleSetTask} />
                                {tasks && tasks.map(t => (<Task key={t.id} task={t} allTags={tags} setAllTags={setTags}  allPriorities={priorities} handleDelete={handleDeleteTask} handleComplete={handleCompleteTask} />))}
                            </>
                        }
                    </div>
                }

            </div>
        </div>


    )
}

export default Dashboard