import React from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import { useRecoilValue } from 'recoil';
import { isSuperAdminState } from '../store/keycloakState';

const SuperAdminLayout: React.FC = () => {
	const navigate = useNavigate();

	const isSuperAdmin = useRecoilValue(isSuperAdminState);
	if (!isSuperAdmin) {
		navigate('/notFound');
		return <></>;
	}

	return <Outlet />;
};

export default SuperAdminLayout;
