import { TableColumnData, TableRowData } from "@/types/components/table"

import styles from './Table.module.scss'

type Props = {
    columns: TableColumnData[]
    rows: TableRowData[]
}

const Table = (props: Props) => {
    const {
        columns,
        rows
    } = props

    const renderRows = () => {
        if (rows.length <= 0) {
            return (
                <tr>
                    <td colSpan={columns.length} style={{ textAlign: 'center', height: '10em' }}>No data</td>
                </tr>
            )
        }
        return (
            rows.map((row) => (
                <tr key={row.id}>
                    {columns.map((column) => (
                        <td key={column.key}>{typeof row[column.key] === 'boolean' ? row[column.key].toString() : row[column.key]}</td>
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
                            <td key={column.key}>{column.name}</td>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {renderRows()}
                </tbody>
            </table>
        </div>
    )
}

export default Table