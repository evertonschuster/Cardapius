import Button from 'antd/es/button'
import { MenuFoldOutlined, MenuUnfoldOutlined, } from '@ant-design/icons';

import React from 'react'
import theme from 'antd/es/theme';
import { Header as HeaderAntd } from 'antd/es/layout/layout';

interface HeaderProps {
    collapsed: boolean;
    setCollapsed: (action: (state: boolean) => boolean) => void;
}

export const Header: React.FC<HeaderProps> = (props) => {

    const { token: { colorBgContainer }, } = theme.useToken();

    return (
        <HeaderAntd style={{ padding: 0, background: colorBgContainer }}>
            <Button
                type="text"
                icon={props.collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
                onClick={() => props.setCollapsed(old => !old)}
                style={{
                    fontSize: '16px',
                    width: 64,
                    height: 64,
                }}
            />
        </HeaderAntd>
    )
}
