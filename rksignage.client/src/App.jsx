import { useEffect, useState } from 'react';
import './App.css';
import PhotoViewer from "./Components/PhotoViewer";

function App() {
    const [slides, setSlides] = useState();
    const playListName = 'TestPlayList1';

    const testSlides = [
        "https://localhost:7174/StaticFiles/River1.jpg",
        "https://localhost:7174/StaticFiles/River2.jpg",
        "https://localhost:7174/StaticFiles/River3.jpg",
        "https://localhost:7174/StaticFiles/River4.jpg"
    ]

    useEffect(() => {
        getSlidesData();
    }, []);

    return (
        <div>
             <PhotoViewer _data={slides} > </PhotoViewer>
        </div>
    );
    //  <img src="https://localhost:7174/StaticFiles/Menu1.png" class="img" alt="Menu" />

    async function getSlidesData() {
        try {
            const response = await fetch(`GetSlides?playListName=${playListName}`)

            if (!response.ok) {
                throw new Error(response.statusText);
            }
            const data = await response.json();

            if (data.length == 0) {
                console.log("invalid Table Name");
                return;
            }
            else {
                ParseData(data);
            }

        } catch (e) {
            console.log(e);
        }
    }

    function ParseData(unParsedData) {
        let cleanData = []

        unParsedData.forEach(obj => {
            cleanData.push(obj.path);
        });

        setSlides(cleanData);
    }
}

export default App;