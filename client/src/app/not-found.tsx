import Link from 'next/link'
import Button from '@/components/shared/Button'
 
export default async function NotFound() {

  return (
    <div>
      <h2>404 Not Found</h2>
      <p>Could not find requested resource</p>
      <p>
        <Button>
            <Link href="/">Go back</Link>
        </Button>
      </p>
    </div>
  )
}
