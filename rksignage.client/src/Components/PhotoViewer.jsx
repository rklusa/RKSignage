import { Fade } from "react-slideshow-image";
import "react-slideshow-image/dist/styles.css";

function PhotoSlider({ _data }) {

    if (_data == undefined) {
        return (
            <h1>data is undefined! reload the page</h1>
        )
    }
    else {
        return (
            <div>
                <Fade>
                    {_data.map((slideObj, index) => {
                        return (
                            <div key={index}>
                                <img src={_data[index]} className="img" alt="Menu" />
                            </div>
                        );
                    })}
                </Fade>
            </div>
        )
    }
    
}

export default PhotoSlider;