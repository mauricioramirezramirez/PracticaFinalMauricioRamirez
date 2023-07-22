
import { Button, Table } from "reactstrap";


const PersonalItemTable = ({ personalItems, deletePersonalItem, setEdit, showModal, setShowModal }) => {
    const sendData = (contacto) => {
        setEdit(contacto);
        setShowModal(!showModal);
    };

    const notData = (
        <tr>
            <td colSpan="5" className="text-center p-3">No hay elementos registrados</td>
        </tr>
    );

    const data = (
        personalItems.map((item) => (
            <tr key={item.Id}>
                <td>{item.Id}</td>
                <td>{item.Name}</td>
                <td>{item.Description}</td>
                <td>{item.IsCompleted ? "Completado" : "Falta por completar"}</td>
                <td>
                    <div className='d-flex justify-content-between'>
                        <Button color="danger" size="sm" className="me-2" onClick={() => deletePersonalItem(item.Id)}>Eliminar</Button>
                        <Button color="primary" size="sm" className="me-2" onClick={() => sendData(item)}>Editar</Button>
                    </div>
                </td>
            </tr>
        ))
    );


    return (
        <>
            <Table striped responsive id="table-to-xls">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Nombre</th>
                        <th>Descripción</th>
                        <th>Completado</th>
                        <th style={{ 'width': '160px' }}>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    {(personalItems.length === 0) ? notData : data}
                </tbody>
            </Table>
        </>
    );
}

export default PersonalItemTable;


