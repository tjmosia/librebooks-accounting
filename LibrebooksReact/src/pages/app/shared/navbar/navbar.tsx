import { Button, Menu, MenuItem, MenuList, MenuPopover, MenuTrigger, tokens, Toolbar, ToolbarButton, ToolbarGroup } from "@fluentui/react-components";
import type { FC } from "react";
import { MdAccountCircle, MdDashboard, MdLogout, MdPerson } from "react-icons/md";
import './navbar.scss'
import { useIdentityService } from "../../../../hooks";
import { useNavigate } from "react-router";

export const Navbar: FC = () => {
    const navigate = useNavigate()
    const { logout } = useIdentityService()

    return <>
        <header className={`app-navbar`}>
            <Toolbar className="navbar-toolbar">
                <ToolbarGroup>
                    <Button onClick={() => navigate("/app")} appearance="subtle" icon={<MdDashboard />} />
                </ToolbarGroup>
                <ToolbarGroup>
                </ToolbarGroup>
                <ToolbarGroup className="toolbar-group--session">
                    <Menu positioning={"below-end"}>
                        <MenuTrigger>
                            <ToolbarButton icon={<MdAccountCircle />} />
                        </MenuTrigger>
                        <MenuPopover>
                            <MenuList>
                                <MenuItem icon={<MdPerson />}>Profile</MenuItem>
                                <MenuItem onClick={() => logout()} icon={<MdLogout />}>Logout</MenuItem>
                            </MenuList>
                        </MenuPopover>
                    </Menu>
                </ToolbarGroup>
            </Toolbar>
        </header>
    </>
}
