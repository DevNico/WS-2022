import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useRecoilValue } from 'recoil';
import { isSuperAdminState } from '../store/keycloakState';
import BaseLayout from './BaseLayout';
import AdminDrawer from './components/AdminDrawer';

const SuperAdminLayout: React.FC = () => {
	const navigate = useNavigate();

	const isSuperAdmin = useRecoilValue(isSuperAdminState);
	if (!isSuperAdmin) {
		navigate('/notFound');
		return <></>;
	}

	return <BaseLayout drawer={<AdminDrawer />} />;
};

export default SuperAdminLayout;
