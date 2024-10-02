import Header from "../components/Header";
import Menu from "../components/Menu";

const Dashboard = () => {
    return (
        <div className="bg-[#F3EDFF] w-full min-h-screen flex">
            <Menu />
            <div className="flex-1 flex flex-col">
                <Header />
            </div>
        </div>
    );
};

export default Dashboard;
