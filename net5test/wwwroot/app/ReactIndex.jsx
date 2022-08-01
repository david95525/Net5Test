import ReactDOM from 'react-dom';
import React, { useState, useEffect } from "react";

function BloodPressure() {
    const [error, Seterror] = useState(false);
    const [isLoaded, SetLoaded] = useState(false);
    const [bplist, Setbp] = useState([]);
    useEffect(() => {
        let headers = {
            "Content-Type": "application/json",
            "Accept": "application/json",
        }
        fetch("/BloodPressure/GetBpmData", {
            method: 'POST',
            headers: headers
        })
            .then(res => res.json())
            .then(
                (result) => {
                    SetLoaded(true);
                    Setbp(result.data);  
                },
                (error) => {
                    SetLoaded(true);
                    Seterror(true);
                }
        )    
    }, bplist);
    if (error) {
        return <div>Error: {error.message}</div>;
    } else if (!isLoaded) {
        return <div>Loading...</div>;
    } else {
        return (
            <table className="table table-bordered table-hover" style={{ width: "100 %" }} >
                <thead>
                    <tr>
                        <td>email</td>
                        <td>name</td>
                        <td>sys</td>
                        <td>dia</td>
                        <td>pul</td>
                        <td>update date</td>
                    </tr>
                </thead>
                <tbody>
                    {bplist.map(item => (
                        <tr key={item.bpm_id}>
                            <td >
                                {item.email}
                            </td>
                            <td >
                                {item.name}
                            </td>
                            <td >
                                {item.sys}
                            </td>
                            <td >
                                {item.dia}
                            </td>
                            <td >
                                {item.pul}
                            </td>
                            <td >
                                {item.update_date}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    }
}
const container = document.getElementById('main');
const root = ReactDOM.createRoot(container);
    root.render(
        <BloodPressure/>);