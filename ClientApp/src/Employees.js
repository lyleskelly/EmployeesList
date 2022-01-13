import React, { useState, useEffect } from 'react';

function Employees() {

    const [employees, setEmployees] = useState([]);

    const getEmployees = () => {
        fetch('/employees')
            .then(response => {
                return response.json()
            })
            .then(data => {
                setEmployees(data)
            })
            .catch(error => console.log(error.message));
    }

    useEffect(() => {
        getEmployees()
    }, []);


    return (
        <div>
            <h2>Employees</h2>

            <table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Value</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map((employee, id) => (
                        <tr key={id}>
                            <td>{employee.id}</td>
                            <td>
                                <input type="text" value={employee.name}/>
                            </td>
                            <td>
                                <input type="text" value={employee.value} />
                            </td>
                            <td>
                                <button type="button" onClick={() => updateEmployee(employee)}>Update</button>
                                <button type="button" onClick={() => deleteEmployee(employee.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                            <input />
                        </td>
                        <td>
                            <input />
                        </td>
                        <td>
                            <button>Add</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}

export default Employees;