import React from 'react';
import { Route, Routes } from 'react-router-dom';
import HomeLayout from './layouts/HomeLayout';
import HomePage from './pages/HomePage';
import NotFoundPage from './pages/NotFoundPage';
import OrganisationsPage from './pages/OrganisationsPage';
import CreateUserPage from './pages/CreateUserPage';
import UnauthorizedPage from './pages/UnauthorizedPage';
import CreateRolePage from './pages/CreateRolePage';
import UsersPage from './pages/UsersPage';
import RolesPage from './pages/RolesPage';
import CreateOrganisationPage from './pages/CreateOrganisationPage';

const Router: React.FC = () => {
	return (
		<Routes>
			<Route path='/' element={<HomeLayout />}>
				<Route index element={<HomePage />} />
				<Route path='/organisation'>
					<Route index element={<OrganisationsPage />} />
					<Route path='create' element={<CreateOrganisationPage />} />
					<Route path=':name'>
						<Route path='user'>
							<Route index element={<UsersPage />} />
							<Route
								path=':create'
								element={<CreateUserPage />}
							/>
						</Route>
						<Route path='role'>
							<Route index element={<RolesPage />} />
							<Route path='create' element={<CreateRolePage />} />
						</Route>
					</Route>
				</Route>

				<Route path='/unauthorized' element={<UnauthorizedPage />} />
			</Route>
			<Route path='*' element={<NotFoundPage />} />
		</Routes>
	);
};

export default Router;
