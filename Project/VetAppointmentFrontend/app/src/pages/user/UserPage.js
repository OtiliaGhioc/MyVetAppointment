import {
    useParams
} from "react-router-dom";

const UserPage = () => {
    let { id } = useParams();

    return (
        <>
            <h1>User page with id: {id}</h1>
        </>
    )
}

export default UserPage;