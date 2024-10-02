import LoginImage from '../assets/login-img.png';
import Input from '../components/Input';
import React, { ChangeEvent } from 'react';
import EmailIcon from '@mui/icons-material/Email';
import VisibilityIcon from '@mui/icons-material/Visibility';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import Button from '../components/Button';

const Login = () => {
    const [email, setEmail] = React.useState('');
    const [senha, setSenha] = React.useState('');
    const [showPassword, setShowPassword] = React.useState(false);

    const handleEmailChange = (event: ChangeEvent<HTMLInputElement>) => {
        setEmail(event.target.value);
    };

    const handleSenhaChange = (event: ChangeEvent<HTMLInputElement>) => {
        setSenha(event.target.value);
    };

    const togglePasswordVisibility = () => {
        setShowPassword(prevState => !prevState)
    }

    const Click = () => {
        console.log("clicou")
    }

    return (
        <div className="bg-[#F3EDFF] w-full min-h-screen flex overflow-hidden"> 
            <div className="flex flex-1 items-center justify-center h-screen"> 
                <div className="max-w-md w-full px-4"> 
                    <div className=''>
                        <h1 className="font-['Titillium_Web'] text-black font-bold text-[36px] mb-[16px]">Acesse a plataforma</h1>
                        <p className="font-['Titillium_Web'] text-[#475569] text-[16px] mb-[35px]">Bem-vindo ao Controla Aí! Faça login e assuma o controle total de tudo, gerenciando suas tarefas de forma prática e eficiente.</p>
                    </div>        
                    <div className='mb-[32px]'>
                        <Input name={'E-mail'} text={'E-mail'} type={'Text'} placeholder='Digite seu e-mail' onChange={handleEmailChange} value={email} icon={<EmailIcon/>}/>
                        <Input name={'Password'} text={'Password'} type={showPassword ? 'text' : 'password'} placeholder='Digite sua senha' onChange={handleSenhaChange} value={senha} 
                        icon={
                            showPassword ? (
                                <VisibilityOffIcon onClick={togglePasswordVisibility} style={{ cursor: 'pointer' }} />
                            ) : (
                                <VisibilityIcon onClick={togglePasswordVisibility} style={{ cursor: 'pointer' }} />
                            )
                        }/>
                    </div>  
                    <Button text={'Entrar'} onClick={Click}/>    
                    <div className='mt-[30px]'>
                        <p className="font-['Titillium_Web'] text-[#475569]">Ainda não tem uma conta? <a href='' className='text-[#7C3AED] font-bold'>Inscreva-se</a></p>
                    </div>  
                </div>
            </div>

            <div className="flex flex-1 items-center justify-center h-screen"> 
                <img src={LoginImage} alt="Login" className="w-full h-auto object-cover"/>
            </div>
        </div>
    );
};

export default Login;
