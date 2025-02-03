import { useEffect, useState } from 'react';
import './App.css';
import PhotoViewer from "./Components/PhotoViewer";
import PlayListViewer from "./Components/PlayListViewer";

function App() {
    const [slides, setSlides] = useState();
    const [playlists, setPlaylists] = useState();
    const playListName = 'TestPlayList1';

    const testSlides = [
        "https://localhost:7174/StaticFiles/River1.jpg",
        "https://localhost:7174/StaticFiles/River2.jpg",
        "https://localhost:7174/StaticFiles/River3.jpg",
        "https://localhost:7174/StaticFiles/River4.jpg"
    ]

    useEffect(() => {
        //getSlidesData();
        GetPlayListsData();
    }, []);

    return (
        <div>
            <PlayListViewer _data={playlists}> </PlayListViewer>
        </div>
    );

    // <PhotoViewer _data={slides} > </PhotoViewer>
    //  <img src="https://localhost:7174/StaticFiles/Menu1.png" class="img" alt="Menu" />

    async function GetPlayListsData() {
        try {
            const response = await fetch(`GetPlaylists`)

            if (!response.ok) {
                throw new Error(response.statusText);
            }
            const data = await response.json();

            if (data.length == 0) {
                console.log("No Playlists found");
                return;
            }
            else {
                setPlaylists(data);
            }

        } catch (e) {
            console.log(e);
        }
    }
    async function GetSlidesData() {
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