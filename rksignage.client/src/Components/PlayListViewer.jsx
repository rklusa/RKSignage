function PlayListViewer({ _data, _setPlaylistName}) {

    let dataItems = undefined;

    if (_data == undefined) {
        return
    }
    else {
        dataItems = _data.map(item =>
            <li>
                {item.name}
                <button onClick={() => {_setPlaylistName(item.name)}}> Play </button>
            </li>
        );
    }
    
    if (_data == undefined) {
        return (
            <p1>data is undefined! reload the page</p1>
        )
    }
    else {
        return (
            <div>
                <h1> PlayLists: </h1>
                <ul>{dataItems}</ul>
            </div>
        )
    }
}

export default PlayListViewer;