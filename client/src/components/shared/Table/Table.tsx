'use client'

import { TableColumnData, TableRowData } from "@/types/components/table"

import styles from './Table.module.scss'
import { error } from 'console'

type Props = {
    columns: TableColumnData[]
    rows: TableRowData[]
    isLoading?: boolean
    errorMsg?: string
    lastRowHighlight?: boolean
}

const Table = (props: Props) => {
    const {
        columns,
        rows,
        isLoading = false,
        errorMsg = '',
        lastRowHighlight = false
    } = props

    const infoDisplayRow = (msg: string) => (
        <tr>
            <td colSpan={columns.length} style={{ textAlign: 'center', height: '10em' }}>{msg}</td>
        </tr>
    )

    const renderRows = () => {
        if (isLoading) return infoDisplayRow('Loading...')
        if (errorMsg) return infoDisplayRow(errorMsg)
        if (rows.length <= 0) return infoDisplayRow('No data.')
        return (
            rows.map((row, rowIndex) => (
                    <tr key={rowIndex}>
                    {columns.map((column, colIndex) => (
                        lastRowHighlight && rowIndex === rows.length - 1 ? (
                            <td key={`${rowIndex}-${colIndex}`} className={styles.table_header}>{typeof row[column.key] === 'boolean' ? row[column.key].toString() : row[column.key]}</td>
                        ) : (
                            <td key={`${rowIndex}-${colIndex}`}>{typeof row[column.key] === 'boolean' ? row[column.key].toString() : row[column.key]}</td>
                        )
                    ))}
                </tr>
            ))
        )
    }

    return (
        <div className={styles.table_wrapper}>
            <table className={styles.table}>
                <thead>
                    <tr>
                        {columns.map((column) => (
                            <td
                                key={column.key}
                                className={styles.table_header}
                            >
                                {column.name}
                            </td>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {
                        renderRows()
                    }
                </tbody>
            </table>
        </div>
    )
}

export default Table