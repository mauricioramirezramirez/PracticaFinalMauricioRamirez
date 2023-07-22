import { Container, Row, Col, Card, CardHeader, CardBody, Button, Input } from "reactstrap";
import { useEffect, useState } from 'react';
import PersonalItemTable from "./components/PersonalItemTable";
import PersonalItemModal from "./components/PersonalItemModal";


const App = () => {
    const [personalItems, setPersonalItems] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [edit, setEdit] = useState(null);

    // GET
    const getPersonalItems = async () => {
        const response = await fetch("https://localhost:7163/api/PersonalItem");

        if (!response.ok) {
            setPersonalItems([]);
            return;
        }

        const data = await response.json();
        setPersonalItems(data);
    }

    // DELETE
    const deletePersonalItem = async (id) => {
        var confirm = window.confirm("¿Deseas eliminar el elemento?");
        if (!confirm) return;

        const response = await fetch("https://localhost:7163/api/PersonalItem/" + id, {
            method: "DELETE",
        });

        if (!response.ok) return window.alert("No se ha podido eliminar el elemento");

        window.alert("Elemento eliminado");
        getPersonalItems();
    };

    // POST
    const savePersonalItem = async (item) => {
        if (item.hasOwnProperty("Id")) {
            delete item.Id
        }
        item.IsCompleted = Boolean(item.IsCompleted);
        const response = await fetch("https://localhost:7163/api/PersonalItem", {
            method: "POST",
            headers: {
                'Content-Type': "application/json;charset=utf-8",
            },
            body: JSON.stringify(item),
        });

        if (!response.ok) {
            window.alert("No se ha podido registrar el elemento");
            return;
        }

        setShowModal(!showModal);
        getPersonalItems();
    };

    // PUT
    const updatePersonalItem = async (item) => {
        item.IsCompleted = Boolean(item.IsCompleted);
        const response = await fetch("https://localhost:7163/api/PersonalItem/" + item.Id, {
            method: "PUT",
            headers: {
                'Content-Type': "application/json;charset=utf-8",
            },
            body: JSON.stringify(item),
        });


        if (!response.ok) {
            window.alert("No se ha podido actualizar el elemento");
            return;
        }


        setShowModal(!showModal);
        getPersonalItems();
    };

  


    useEffect(() => {
        getPersonalItems()
    }, []);


    return (
        <Container>
            <Row className="mt-5">
                <Col sm="12">
                    <Card>
                        <CardHeader>
                            <div className="d-flex justify-content-between">
                                <h5>Personal items</h5>
                                <div className="d-flex gap-3">
                                    <Button size="sm" color="success" onClick={() => setShowModal(!showModal)}>Agregar</Button>
                                </div>
                            </div>
                        </CardHeader>
                        <CardBody>

                            <PersonalItemTable
                                personalItems={personalItems}
                                deletePersonalItem={deletePersonalItem}
                                setEdit={setEdit}
                                setShowModal={setShowModal}
                                showModal={showModal}
                            />

                        </CardBody>
                    </Card>

                </Col>
            </Row>

            <PersonalItemModal
                showModal={showModal}
                setShowModal={setShowModal}
                savePersonalItem={savePersonalItem}
                edit={edit}
                setEdit={setEdit}
                updatePersonalItem={updatePersonalItem}
            />
        </Container>
    );
}

export default App;
