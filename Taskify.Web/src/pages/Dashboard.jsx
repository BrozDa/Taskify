import { useAuth } from "../context/AuthContext";
import DashboardMenu from '../components/DashboardMenu';
import DashboardHeader from '../components/DashboardHeader';
import { useEffect, useState } from "react";
import { prioritiesGetAll } from "../services/apiPriorities";
import { tagsGetAll } from "../services/apiTags";

function Dashboard() {

    const [priorities, setPriorities] = useState([]);
    const [tags, setTags] = useState([]);

    const handleClick = async() => {
        const priorities = await prioritiesGetAll();
        const tags = await tagsGetAll();

        setPriorities(priorities);
        setTags(tags);
    }
  return (
    <div className="flex min-h-screen bg-gray-100">
        <DashboardMenu />
        <div className="flex flex-col flex-1 overflow-y-auto">
            <DashboardHeader />
            <div className="p-4">
                <button onClick={() => handleClick()}>Click me</button>
                {priorities && <div>
                    <ul>
                        {priorities.map(p => <li key={p.id}>{p.name}</li>)}
                    </ul>
                    </div>}
                <p>AAAAAAAAAAAAA</p>
                {tags && <div>
                    <ul>
                        {tags.map(t => <li key={t.id}>{t.name}</li>)}
                    </ul>
                    </div>}
                <h1 className="text-2xl font-bold">Welcome to my dashboard!</h1>
                <p className="mt-2 text-gray-600">This is an example dashboard using Tailwind CSS.</p>
            </div>
        </div>
    </div>
    
    
  )
}

export default Dashboard