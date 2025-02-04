import Image from "next/image";
import Link from "next/link";

export default function Home() {
  return (


    <>
      <div className="flex p-2 bg-slate-100">

        <div className="flex flex-1 justify-center gap-8">
          <Link href={'/'} className="p-2">Home</Link>
          <Link href={'/'} className="p-2">Dashboard</Link>
        </div>
        <div className="flex gap-4 px-4 py-2">
          <Link href={'/login'}>login</Link>
          <Link href={'/register'}>register</Link>
        </div>
      </div>

      <section className=" flex justify-center">

        <div className=" max-w-6xl flex flex-1 flex-col gap-4 p-4">
          <div className="grid auto-rows-min gap-4 md:grid-cols-5">
            {Array.from({ length: 20 }).map((_, i) => (
              <div key={i} className="m-2 aspect-square rounded-xl bg-muted/50 text-center align-middle  flex justify-center items-center" >
                <div className="">
                  {i}
                </div>

              </div>
            ))}
          </div>
        </div>

      </section>
    </>
  );
}
